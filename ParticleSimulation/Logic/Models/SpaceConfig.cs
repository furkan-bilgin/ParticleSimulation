using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models
{
    public class SpaceConfig
    {
        public float MinimumParticleInteraction { get; set; }
        public float MaximumParticleInteraction { get; set; }

        public float MinimumInitialPositionX { get; set; }
        public float MaximumInitialPositionX { get; set; }
        public float MinimumInitialPositionY { get; set; }
        public float MaximumInitialPositionY { get; set; }

        public int ParticleTypeCount { get; set; }
        public int ParticleCount { get; set; }
    }
}
