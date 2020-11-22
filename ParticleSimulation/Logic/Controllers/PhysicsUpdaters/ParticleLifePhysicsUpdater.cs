using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.ParticleData;
using ParticleSimulation.Logic.Models.Physics;
using ParticleSimulation.Logic.Utils;
using ParticleSimulation.Utils;
using System;
using System.Linq;

namespace ParticleSimulation.Logic.Controllers.PhysicsUpdaters
{
    public class ParticleLifePhysicsUpdater : IPhysicsUpdater
    {
        private float MAXIMUM_DISTANCE;

        public ParticleLifePhysicsUpdater(SpaceConfig config)
        {
            MAXIMUM_DISTANCE = config.MaximumParticleInteractionDistances.Last();
        }

        public void UpdatePhysics(ParticleJobSchedule schedule)
        {
            var particle = schedule.Particle;
            var velocity = new Vector2();

            foreach (var otherParticle in schedule.SpaceSnapshot.AllParticles)
            {
                if (particle.Id == otherParticle.Id)
                    continue;

                var a = particle.Position;
                var b = otherParticle.Position;

                var sqrDistance = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
                //var sqrDistance = particle.Position.DistanceSquared(otherParticle.Position);
                if (sqrDistance > MAXIMUM_DISTANCE)
                    continue;

                var delta = (particle.Position - otherParticle.Position).Normalize();
                var particleData = particle.GetParticleData<ParticleLifeParticleData>().ParticleType;
                var interactionConstant = particleData.ParticleInteractions[particleData.Id]
                                                      .GetInteraction(sqrDistance);

                if (delta == Vector2.Zero || delta.IsNaN())
                {
                    if (particle.Id > otherParticle.Id)
                        delta = RNG.RandomBool() ? new Vector2(-1, 0) : RNG.RandomBool() ? new Vector2(-1, -1) : new Vector2(-1, 1);
                    else
                        delta = RNG.RandomBool() ? new Vector2(1, 0) : RNG.RandomBool() ? new Vector2(1, 1) : new Vector2(1, -1);
                }

                delta = delta * interactionConstant;
                velocity += delta;
            }

            particle.Velocity = velocity.Average(particle.Velocity);

            particle.Velocity = particle.Velocity.Lerp(Vector2.Zero, 0.05f); // Friction
            particle.ScheduledPosition += particle.Velocity * LogicTimer.FixedDelta * 50;
        }
    }
}
