using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models
{
    public class SpaceConfig
    {
        /// Following 4 lists are about particle interactions. TODO: Detail
        public List<float> MinimumParticleInteractions { get; set; }
        public List<float> MaximumParticleInteractions { get; set; }

        public List<float> MinimumParticleInteractionDistances { get; set; }
        public List<float> MaximumParticleInteractionDistances { get; set; }

        public float MinimumInitialPositionX { get; set; }
        public float MaximumInitialPositionX { get; set; }
        public float MinimumInitialPositionY { get; set; }
        public float MaximumInitialPositionY { get; set; }

        public int ParticleTypeCount { get; set; }
        public int ParticleCount { get; set; }

        public int BatchCount { get; set; }
    }
}
