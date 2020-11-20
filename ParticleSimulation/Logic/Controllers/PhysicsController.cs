using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Physics;
using ParticleSimulation.Utils;
using System;

namespace ParticleSimulation.Logic.Controllers
{
    public class PhysicsController
    {
        private const float GO_AWAY_DISTANCE = 8;
        private const float GO_AWAY_DISTANCE_SQR = GO_AWAY_DISTANCE * GO_AWAY_DISTANCE;
        private const float MAXIMUM_DISTANCE = 100 * 100;
        
        public ParticleJobSchedule ScheduleParticleJob(Particle particle, Space space)
        {
            var schedule = new ParticleJobSchedule()
            {
                Particle = particle,
                SpaceSnapshot = space.CurrentSpaceSnapshot
            };

            return schedule;
        }

        /// <summary>
        /// Update particle with given ParticleJobSchedule
        /// </summary>
        public void UpdateParticle(ParticleJobSchedule schedule)
        {
            var particle = schedule.Particle;
            var velocity = new Vector2();

            foreach (var otherParticle in schedule.SpaceSnapshot.AllParticles)
            {
                if (particle.Id == otherParticle.Id)
                    continue;

                var sqrDistance = particle.Position.DistanceSquared(otherParticle.Position);
                if (sqrDistance > MAXIMUM_DISTANCE)
                    continue;

                var distance = 1 / sqrDistance * 150; // Distance formula for reducing effects of interactions for particles far away.

                if (float.IsNaN(distance) || float.IsInfinity(distance))
                    distance = 1;

                var delta = (particle.Position - otherParticle.Position).Normalize();
                var interactionConstant = particle.ParticleType.ParticleInteractions[otherParticle.ParticleType.Id];

                var isTooClose = sqrDistance < GO_AWAY_DISTANCE_SQR;

                if (delta == Vector2.Zero || delta.IsNaN() || isTooClose)
                {
                    delta = particle.Id > otherParticle.Id ? -Vector2.UnitX : Vector2.UnitX;
                }

                if (isTooClose)
                {
                    distance = 1;
                    interactionConstant = (GO_AWAY_DISTANCE_SQR - sqrDistance) / (GO_AWAY_DISTANCE_SQR);
                }

                delta = delta * interactionConstant;

                velocity -= delta * distance.Clamp(0, 1);
                
            }

            particle.Velocity = velocity.Average(particle.Velocity);//.Clamp(-Vector2.One, Vector2.One);

            particle.Velocity = particle.Velocity.Lerp(Vector2.Zero, 0.05f); // Friction
            particle.ScheduledPosition += particle.Velocity * LogicTimer.FixedDelta * 50;

        }
    }
}
