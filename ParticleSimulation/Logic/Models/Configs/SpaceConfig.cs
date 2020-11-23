using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models.Configs
{
    public class SpaceConfig
    {
        public float MinimumInitialPositionX { get; set; }
        public float MaximumInitialPositionX { get; set; }
        public float MinimumInitialPositionY { get; set; }
        public float MaximumInitialPositionY { get; set; }

        public int ParticleCount { get; set; }

        public bool AutoGenerateParticleData { get; set; }
        public int GeneratedParticleDataCount { get; set; }

        public int BatchCount { get; set; }
    }
}
