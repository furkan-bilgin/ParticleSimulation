using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models.ParticleData
{
    public class ParticleLifeParticleData : IParticleData
    {
        public ParticleType ParticleType { get; private set; }

        public ParticleLifeParticleData(ParticleType particleType)
        {
            ParticleType = particleType;
        }
    }
}
