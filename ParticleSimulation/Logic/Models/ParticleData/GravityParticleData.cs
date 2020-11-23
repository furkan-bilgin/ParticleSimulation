using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models.ParticleData
{
    public class GravityParticleData : IParticleData
    {
        public float Mass { get; private set; }
        public float Radius { get; private set; }
        public float RadiusSquare { get; private set; }

        public Color Color { get; set; }

        public GravityParticleData(float mass, float radius, Color color)
        {
            Radius = radius;
            RadiusSquare = Radius * Radius;

            Mass = mass;
            Color = color;
        }
    }
}
