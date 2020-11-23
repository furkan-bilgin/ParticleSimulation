using ParticleSimulation.Logic.Controllers.ParticleControllers;
using ParticleSimulation.Logic.Controllers.PhysicsUpdaters;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Controllers
{
    public class LogicController : Singleton<LogicController>
    {
        public ParticleController ParticleController { get; private set; }
        public SpaceController SpaceController { get; private set; }
        public PhysicsController PhysicsController { get; private set; }
        public TaskController TaskController { get; private set; }

        public SpaceConfig CurrentConfig { get; private set; }

        public LogicController(SpaceConfig spaceConfig)
        {
            InitSingleton(this);

            CurrentConfig = spaceConfig;
            IPhysicsUpdater physicsUpdater;

            switch (spaceConfig)
            {
                case ParticleLifeConfig c:
                    ParticleController = new ParticleLifeParticleController(spaceConfig);
                    physicsUpdater = new ParticleLifePhysicsUpdater(c);
                    break;
                case GravityConfig c:
                    ParticleController = new GravityParticleController(spaceConfig);
                    physicsUpdater = new GravityPhysicsUpdater(c);
                    break;

                default:
                    throw new System.Exception("Unknown config type: " + spaceConfig.GetType().Name);
            }


            SpaceController = new SpaceController(spaceConfig);
            PhysicsController = new PhysicsController(spaceConfig, physicsUpdater);
            TaskController = new TaskController(spaceConfig.BatchCount);
        }

        public Space StartLogic()
        {
            return SpaceController.CreateSpace();
        }

        public void UpdateLogic(Space space)
        {
            space.CurrentSpaceSnapshot = SpaceController.CreateSpaceSnapshot(space.Particles);
            var schedules = space.Particles.Select(x => PhysicsController.ScheduleParticleJob(x, space)).ToList(); // Generate schedules for each particle
            TaskController.RunSchedules(schedules); // Run those schedules

            foreach (var particle in space.Particles)
            {
                if (particle.ScheduledPosition.IsNaN())
                    continue;

                particle.Position = particle.ScheduledPosition; // Update particle positions
            }
        }
    }
}
