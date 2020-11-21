using Newtonsoft.Json;
using ParticleSimulation.Logic.Controllers;
using ParticleSimulation.Rendering;
using ParticleSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Logic.Models.SpaceConfig()
            {
                ParticleTypeCount = 5,
                ParticleCount = 2500,

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

                MinimumInitialPositionX = 50,
                MaximumInitialPositionX = 1400,

                MinimumInitialPositionY = 100,
                MaximumInitialPositionY = 900,

                BatchCount = 512
            };

            File.WriteAllText("config.json", JsonConvert.SerializeObject(config, Formatting.Indented));

            var logicController = new LogicController(config);
            var window = new Window();
            var space = logicController.StartLogic();

            window.Run();
            //window.InitShapes(space.Particles);

            var stopwatch = new Stopwatch();

            var logicTimer = new LogicTimer(() =>
            {
                stopwatch.Restart();

                logicController.UpdateLogic(space);

                stopwatch.Stop();
                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            });

            var windowTimer = new LogicTimer(() =>
            {
                stopwatch.Restart();

                //window.UpdateShapePositions(space.Particles);
                window.Update(space.Particles);

                stopwatch.Stop();
            });

            logicTimer.Start();
            windowTimer.Start();

            while (true)
            {
                logicTimer.Update();
                windowTimer.Update();
            }
        }
    }
}
