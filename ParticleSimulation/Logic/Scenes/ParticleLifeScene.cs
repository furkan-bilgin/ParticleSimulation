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
    public class ParticleLifeScene : IScene
    {
        public SpaceConfig Config 
        {
            get => 
                new ParticleLifeConfig()
                {
                    AutoGenerateParticleData = true,
                    GeneratedParticleDataCount = 8,

                    ParticleTypeCount = 7,
                    ParticleCount = 6000,

                    MinimumParticleInteractions = new List<float>()
                    {
                        1, 0, -1, 0
                    },

                    MaximumParticleInteractions = new List<float>()
                    {
                        1, 0, 1, 0
                    },

                    MinimumParticleInteractionDistances = new List<float>()
                    {
                        0, 3*3, 6*6, 8*8
                    },

                    MaximumParticleInteractionDistances = new List<float>()
                    {
                        0, 6*6, 10*10, 40*40
                    },

                    MinimumInitialPositionX = 100,
                    MaximumInitialPositionX = 1400,

                    MinimumInitialPositionY = 100,
                    MaximumInitialPositionY = 800,

                    BatchCount = 400
                };

            set { return; } 
        }

        public void Initialize(LogicController logicController, Space space)
        {
            Console.WriteLine("Go!");
        }
    }
}
