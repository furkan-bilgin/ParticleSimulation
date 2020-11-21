using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Utils
{
    public static class RNG
    {
        private static ThreadLocal<Random> random = new ThreadLocal<Random>(() => new Random());

        public static float RandomFloat(float min, float max)
        {
            return Convert.ToSingle(random.Value.NextDouble() * (max - min) + min);
        }

        public static int RandomInt(int min, int max)
        {
            return random.Value.Next(min, max);
        }

        public static bool RandomBool()
        {
            return RandomFloat(0, 1) >= 0.5f;
        }

        public static T RandomElement<T>(List<T> list)
        {
            return list[random.Value.Next(0, list.Count)];
        }
    }
}
