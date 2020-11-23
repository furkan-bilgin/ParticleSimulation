using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models.Configs
{
    public class GravityConfig : SpaceConfig
    {
        public float MinimumMass { get; set; }
        public float MaximumMass { get; set; }

        public float MinimumInitialVelocity { get; set; }
        public float MaximumInitialVelocity { get; set; }

    }
}
