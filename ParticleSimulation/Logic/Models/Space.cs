using ParticleSimulation.Logic.Models.Physics;
using System.Collections.Generic;

namespace ParticleSimulation.Logic.Models
{
    public class Space
    {
        public SpaceConfig SpaceConfig { get; private set; }
        public List<Particle> Particles { get; private set; }
        public SpaceSnapshot CurrentSpaceSnapshot { get; set; }
        
        public Space(SpaceConfig config, List<Particle> particles)
        {
            SpaceConfig = config;
            Particles = particles;
        }
    }
}
