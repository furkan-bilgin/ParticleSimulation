using ParticleSimulation.Logic.Controllers.PhysicsUpdaters;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.Physics;

namespace ParticleSimulation.Logic.Controllers
{
    public class PhysicsController
    {
        private IPhysicsUpdater physicsUpdater;
        private bool didRunCache;

        public PhysicsController(SpaceConfig config, IPhysicsUpdater physicsUpdater)
        {
            this.physicsUpdater = physicsUpdater;
        }

        public ParticleJobSchedule ScheduleParticleJob(Particle particle, Space space)
        {
            if (!didRunCache) 
            {
                physicsUpdater.CacheParticleData(space.CurrentSpaceSnapshot);
                didRunCache = true;
            }

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
            physicsUpdater.UpdatePhysics(schedule);
        }
    }
}
