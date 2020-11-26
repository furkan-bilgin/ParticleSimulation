using Ara3D;
using Newtonsoft.Json;
using ParticleSimulation.Logic.Controllers;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.ParticleData;
using ParticleSimulation.Logic.Scenes;
using ParticleSimulation.Rendering;
using ParticleSimulation.Utils;
using SFML.Graphics;
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
            var config = new GravityConfig()
            {
                AutoGenerateParticleData = true,
                GeneratedParticleDataCount = 75,

                ParticleCount = 1500,

                MinimumInitialPositionX = 0,
                MaximumInitialPositionX = 15000,

                MinimumInitialPositionY = 0,
                MaximumInitialPositionY = 15000,

                MinimumMass = 6,
                MaximumMass = 150,


                MinimumInitialVelocity = -50,
                MaximumInitialVelocity = 50,

                BatchCount = 125
            };
            
            //var scene = new TwoBodyCollisionScene();
            //var config = scene.Config;
            
            var logicController = new LogicController(config);
            var window = new Window(config);
            var space = logicController.StartLogic();

            space.Particles.Add(
                    logicController.ParticleController.CreateParticle(new GravityParticleData(1000000, 850, Color.Yellow), new Vector2(7400, 7400))
                );

            space.Particles.Add(
                    logicController.ParticleController.CreateParticle(new GravityParticleData(1100000, 750, Color.Blue), new Vector2(9400, 7400))
                );

            space.Particles.Add(
                    logicController.ParticleController.CreateParticle(new GravityParticleData(1100000, 950, Color.Red), new Vector2(8400, 5400))
                );
            //scene.Initialize(logicController, space);

            window.Run();

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
                    if (ms.Count > 0)
                        Console.WriteLine("Average FPS: " + ms.Average());
                    ms.Clear();
                }

                stopwatch.Stop();

                ms.Add(1000.0 / stopwatch.Elapsed.TotalMilliseconds);

                LogicTimer.DeltaTime = 1f / (1000f / (float)stopwatch.Elapsed.TotalMilliseconds);

                if (LogicTimer.DeltaTime < LogicTimer.FixedDelta)
                    LogicTimer.DeltaTime = LogicTimer.FixedDelta;
            });

            timer.Start();
            
            while (true)
            {
                timer.Update();
            }
        }
    }
}
