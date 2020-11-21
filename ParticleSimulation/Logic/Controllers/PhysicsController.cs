using ParticleSimulation.Logic.Controllers.PhysicsUpdaters;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Physics;

namespace ParticleSimulation.Logic.Controllers
{
    public class PhysicsController
    {
        private IPhysicsUpdater physicsUpdater;

        public PhysicsController(SpaceConfig config)
        {
            physicsUpdater = new ParticleLifePhysicsUpdater(config);
        }

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
            physicsUpdater.UpdatePhysics(schedule);
        }
    }
}
