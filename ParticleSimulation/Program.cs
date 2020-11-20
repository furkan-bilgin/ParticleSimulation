using ParticleSimulation.Logic.Controllers;
using ParticleSimulation.Rendering;
using ParticleSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                ParticleTypeCount = 6,
                ParticleCount = 100,

                MinimumParticleInteraction = -1,
                MaximumParticleInteraction = 1,

                MinimumInitialPositionX = 150,
                MaximumInitialPositionX = 1400,

                MinimumInitialPositionY = 150,
                MaximumInitialPositionY = 800
            };

            var logicController = new LogicController(config);
            var window = new Window();
            var space = logicController.StartLogic();

            window.Run();
            window.InitShapes(space.Particles);

            var stopwatch = new Stopwatch();

            var logicTimer = new LogicTimer(() =>
            {
                stopwatch.Restart();

                logicController.UpdateLogic(space);

                stopwatch.Stop();
                Console.WriteLine("LogicTimer took " + stopwatch.ElapsedMilliseconds + "!");
            });

            var windowTimer = new LogicTimer(() =>
            {
                stopwatch.Restart();

                window.UpdateShapePositions(space.Particles);
                window.Update();

                stopwatch.Stop();
                Console.WriteLine("Windowtimer took " + stopwatch.ElapsedMilliseconds + "!");
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
