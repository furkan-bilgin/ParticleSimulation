using SFML.Graphics;
using System.Collections.Generic;

namespace ParticleSimulation.Logic.Models
{
    public class ParticleType
    {
        /// <summary>
        /// Represents attraction or repulsions for the other particles exist in the space.
        /// Key is ParticleType id, Value is positive if attraction, otherwise repulsion.
        /// </summary>
        public Dictionary<int, float> ParticleInteractions { get; private set; }
        public Color Color { get; private set; }
        public int Id { get; private set; }

        public ParticleType(int id, Dictionary<int, float> particleInteractions, Color color)
        {
            this.Id = id;
            this.ParticleInteractions = particleInteractions;
            this.Color = color;
        }
    }
}
