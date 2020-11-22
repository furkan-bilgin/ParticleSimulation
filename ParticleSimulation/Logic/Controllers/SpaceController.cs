using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Physics;
using System.Collections.Generic;
using System.Linq;

namespace ParticleSimulation.Logic.Controllers
{
    public class SpaceController
    {
        private SpaceConfig spaceConfig;

        public SpaceController(SpaceConfig spaceConfig)
        {
            this.spaceConfig = spaceConfig;
        }        

        public Space CreateSpace()
        {
            var initialParticles = LogicController.Instance.ParticleController.GenerateInitialParticles();
            var space = new Space(spaceConfig, initialParticles);

            return space;
        }

        public SpaceSnapshot CreateSpaceSnapshot(List<Particle> particles)
        {
            var spaceSnapshot = new SpaceSnapshot()
            {
                AllParticles = particles.ToArray()//particles.Select(x => x.Clone()).ToList()
            };

            return spaceSnapshot;
        }
    }
}
