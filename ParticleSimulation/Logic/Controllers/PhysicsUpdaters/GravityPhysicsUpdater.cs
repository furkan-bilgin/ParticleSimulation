using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.ParticleData;
using ParticleSimulation.Logic.Models.Physics;
using ParticleSimulation.Logic.Utils;
using ParticleSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParticleSimulation.Logic.Controllers.PhysicsUpdaters
{
    public class GravityPhysicsUpdater : IPhysicsUpdater
    {
        public GravityPhysicsUpdater(SpaceConfig config)
        {
            particleDataCache = new Dictionary<int, GravityParticleData>();
        }

        private Dictionary<int, GravityParticleData> particleDataCache;
        public void CacheParticleData(SpaceSnapshot snapshot)
        {
            foreach (var particle in snapshot.AllParticles)
            {
                particleDataCache[particle.Id] = particle.GetParticleData<GravityParticleData>();
            }
        }

        const float G = 6.674f * (10 ^ 11);
        public void UpdatePhysics(ParticleJobSchedule schedule)
        {
            var particle = schedule.Particle;
            var velocity = new Vector2();

            var particleData = particleDataCache[particle.Id];

            foreach (var otherParticle in schedule.SpaceSnapshot.AllParticles)
            {
                if (particle.Id == otherParticle.Id)
                    continue;

                var otherParticleData = particleDataCache[otherParticle.Id];

                var a = particle.Position;
                var b = otherParticle.Position;

                var sqrDistance = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);

                var mass1 = particleData.Mass; // particle.GetParticleData<GravityParticleData>().Mass;
                var radius1 = particleData.Radius;

                var mass2 = otherParticleData.Mass; //otherParticle.GetParticleData<GravityParticleData>().Mass;
                var radius2 = otherParticleData.Radius;

                var force = G * mass1 * mass2 / sqrDistance; // Calculate Force by Newton's formula
                var acceleration = force / mass1;

                if (float.IsNaN(force))
                {
                    continue;
                }

                if (IsColliding(sqrDistance, radius1, radius2))
                {
                    var overlap = (radius1 + radius2) - sqrDistance.Sqrt();

                    if (overlap.IsNaN())
                        overlap = 0;

                    particle.ScheduledPosition += (a - b).Normalize() * overlap; // Go back a little if we are interlapping
                    
                    var newVelocity = particle.Velocity.Lerp(-otherParticle.Velocity, mass2 / mass1) * (a - b).Normalize();
                    
                    particle.ScheduledVelocity = newVelocity;
                }
                else
                {
                    velocity -= (a - b).Normalize() * acceleration;
                }

            }

            particle.ScheduledVelocity += velocity;
            particle.ScheduledPosition += particle.ScheduledVelocity * LogicTimer.DeltaTime;
        }

        private bool IsColliding(float distanceSqr, float radius1, float radius2)
        {
            return distanceSqr <= (radius1 + radius2) * (radius1 + radius2);
        }
    }
}
