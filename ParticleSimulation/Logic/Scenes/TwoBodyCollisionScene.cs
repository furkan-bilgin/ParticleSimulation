using Ara3D;
using ParticleSimulation.Logic.Controllers;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.ParticleData;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Scenes
{
    public class TwoBodyCollisionScene : IScene
    {
        public SpaceConfig Config { get => new GravityConfig() { AutoGenerateParticleData = false }; set { return; } }

        public void Initialize(LogicController logicController, Space space)
        {
            var defaultParticleData = new GravityParticleData(5, 5, Color.Blue);
            var otherParticleData = new GravityParticleData(35, 10, Color.Blue);
            //var specialParticle = logicController.ParticleController.CreateParticle(otherParticleData, new Vector2(45, 0));
            //specialParticle.Velocity = new Vector2(15, 0);

            var particles = new List<Particle>()
            {
            //    specialParticle,
                logicController.ParticleController.CreateParticle(defaultParticleData, new Vector2(-35, 0)),
                logicController.ParticleController.CreateParticle(defaultParticleData, new Vector2(0, 35))
            };

            space.Particles.AddRange(particles);
        }
    }
}
