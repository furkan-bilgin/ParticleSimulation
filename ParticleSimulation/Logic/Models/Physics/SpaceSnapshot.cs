using Ara3D;
using System.Collections.Generic;

namespace ParticleSimulation.Logic.Models.Physics
{
    public struct SpaceSnapshot
    {
        /// <summary>
        /// This is a list of dictionary with all existing particle positions, and their interactions with all other particles.
        /// </summary>
        public List<Particle> AllParticles { get; set; }
    }
}
