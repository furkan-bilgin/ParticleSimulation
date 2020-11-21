using Ara3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Models
{
    public struct ParticleInteraction
    {
        public List<Vector2> GraphDots { get; set; }

        public float GetInteraction(float sqrDistance)
        {
            var vector = new Vector2(sqrDistance, 0);
            if (float.IsNaN(sqrDistance))
                sqrDistance = 0;

            for (int i = 0; i < GraphDots.Count; i++)
            {
                var item = GraphDots[i];
                if (i == 0 && sqrDistance <= item.X) // If X is outside of graph, return first dot's Y
                    return item.Y;
                
                if (i == GraphDots.Count -1 && sqrDistance >= item.X) // If X is outside of graph, return first dot's Y
                    return item.Y;

                if (item.X < vector.X)
                    continue;

                var right = item;
                var left = GraphDots[i - 1];

                var lerp = right.Lerp(left, Math.Abs((right.X - sqrDistance) / (right.X - left.X)));
                
                return lerp.Y;
            }

            return float.NaN;
        }
    }
}
