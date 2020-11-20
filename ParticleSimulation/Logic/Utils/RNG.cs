using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Utils
{
    public static class RNG
    {
        private static Random random = new Random();

        public static float RandomFloat(float min, float max)
        {
            return Convert.ToSingle(random.NextDouble() * (max - min) + min);
        }

        public static T RandomElement<T>(List<T> list)
        {
            return list[random.Next(0, list.Count)];
        }
    }
}
