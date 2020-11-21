using SFML.Graphics;
using System.Collections.Generic;

namespace ParticleSimulation.Logic.Models
{
    public class ParticleType
    {
        /// <summary>
        /// Represents attraction or repulsions for the other particles exist in the space.
        /// Key is ParticleType id
        /// </summary>
        public Dictionary<int, ParticleInteraction> ParticleInteractions { get; private set; }
        public Color Color { get; private set; }
        public int Id { get; private set; }

        public ParticleType(int id, Dictionary<int, ParticleInteraction> particleInteractions, Color color)
        {
            this.Id = id;
            this.ParticleInteractions = particleInteractions;
            this.Color = color;
        }
    }
}
