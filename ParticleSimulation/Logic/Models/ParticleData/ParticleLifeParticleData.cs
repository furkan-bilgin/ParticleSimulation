using SFML.Graphics;

namespace ParticleSimulation.Logic.Models.ParticleData
{
    public class ParticleLifeParticleData : IParticleData
    {
        public ParticleType ParticleType { get; private set; }
        public Color Color { get; set; }

        public ParticleLifeParticleData(ParticleType particleType, Color color)
        {
            ParticleType = particleType;
            Color = color;
        }
    }
}
