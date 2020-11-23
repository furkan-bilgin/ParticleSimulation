using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models.Configs
{
    public class ParticleLifeConfig : SpaceConfig
    {
        /// Following 4 lists are about particle interactions. TODO: Detail
        public List<float> MinimumParticleInteractions { get; set; }
        public List<float> MaximumParticleInteractions { get; set; }

        public List<float> MinimumParticleInteractionDistances { get; set; }
        public List<float> MaximumParticleInteractionDistances { get; set; }

        public int ParticleTypeCount { get; set; }
    }
}
