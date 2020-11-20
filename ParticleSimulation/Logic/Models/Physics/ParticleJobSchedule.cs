using Ara3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models.Physics
{
    public struct ParticleJobSchedule
    {
        public Particle Particle { get; set; }
        public SpaceSnapshot SpaceSnapshot { get; set; }
    }
}
