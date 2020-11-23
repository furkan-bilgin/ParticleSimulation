using Newtonsoft.Json;
using ParticleSimulation.Logic.Controllers;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Rendering;
using ParticleSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParticleSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var config = new ParticleLifeConfig()
            {
                AutoGenerateParticleData = true,
                GeneratedParticleDataCount = 8,

                ParticleTypeCount = 7,
                ParticleCount = 1500,

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

                BatchCount = 512
            };*/

            var config = new GravityConfig()
            {
                AutoGenerateParticleData = true,
                GeneratedParticleDataCount = 8,

                ParticleCount = 1000,

                MinimumInitialPositionX = 0,
                MaximumInitialPositionX = 20000,

                MinimumInitialPositionY = 0,
                MaximumInitialPositionY = 20000,

                MinimumMass = 1,
                MaximumMass = 200,

                MinimumInitialVelocity = -50,
                MaximumInitialVelocity = 50,

                BatchCount = 512
            };

            File.WriteAllText("config.json", JsonConvert.SerializeObject(config, Formatting.Indented));

            var logicController = new LogicController(config);
            var window = new Window();
            var space = logicController.StartLogic();

            window.Run();
            //window.InitShapes(space.Particles);

            var stopwatch = new Stopwatch();
            var fpsStopwatch = new Stopwatch();

            var ms = new List<double>();
            fpsStopwatch.Start();
            var timer = new LogicTimer(() =>
            {
                stopwatch.Restart();

                logicController.UpdateLogic(space);
                window.Update(space.Particles);


                if (fpsStopwatch.ElapsedMilliseconds >= 1000)
                {
                    fpsStopwatch.Restart();
                    Console.WriteLine("Average FPS: " + ms.Average());
                    ms.Clear();
                }

                ms.Add(1000.0 / stopwatch.Elapsed.TotalMilliseconds);
            });

            timer.Start();
            
            while (true)
            {
                timer.Update();
            }
        }
    }
}
