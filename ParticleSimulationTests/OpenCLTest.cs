using Ara3D;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParticleSimulation.Logic.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ParticleSimulationTests
{
    [TestClass]
    public class OpenCLTest
    {

        [TestMethod]
        public void TestInvoke()
        {
            var cl = new OpenCLController();
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 10; i++)
            {
                var list = Enumerable.Range(0, 1000).Select(x => new Vector2(x, x + 1));
                var result = cl.CalculateSquareDistance(list.ToList());
            }

            stopwatch.Stop();

            Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
