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

            foreach (var otherParticle in schedule.SpaceSnapshot.AllParticles)
            {
                if (particle.Id == otherParticle.Id)
                    continue;

                var a = particle.Position;
                var b = otherParticle.Position;

                var sqrDistance = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);

                var mass1 = particleDataCache[particle.Id].Mass; // particle.GetParticleData<GravityParticleData>().Mass;
                var mass2 = particleDataCache[otherParticle.Id].Mass; //otherParticle.GetParticleData<GravityParticleData>().Mass;

                var force = G * mass1 * mass2 / sqrDistance;
                var acceleration = force / mass1;

                if (float.IsNaN(force))
                {
                    continue;
                }

                velocity -= (a - b).Normalize() * acceleration;
            }

            particle.Velocity += velocity;
            particle.ScheduledPosition += particle.Velocity * LogicTimer.FixedDelta;
        }
    }
}
