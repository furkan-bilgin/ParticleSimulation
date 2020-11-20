using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Physics;
using ParticleSimulation.Logic.Utils;
using ParticleSimulation.Utils;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ParticleSimulation.Logic.Controllers
{
    public class ParticleController
    {
        private SpaceConfig spaceConfig;

        public ParticleController(SpaceConfig spaceConfig)
        {
            this.spaceConfig = spaceConfig;
        }

        private int _particleId;
        /// <summary>
        /// Generate a particle with given particletype and startPosition
        /// </summary>
        public Particle CreateParticle(ParticleType particleType, Vector2 startPosition)
        {
            var particle = new Particle(++_particleId, particleType, startPosition);
            return particle;
        }

        /// <summary>
        /// Generate initial particles
        /// </summary>
        public List<Particle> GenerateInitialParticles()
        {
            var particleTypes = GenerateParticleTypes(spaceConfig.ParticleTypeCount);
            var initialPositions = GenerateInitialPositions(spaceConfig.ParticleCount);

            return Enumerable.Range(0, spaceConfig.ParticleCount)
                             .Select(x => CreateParticle(RNG.RandomElement(particleTypes), initialPositions[x]))
                             .ToList();
        }



        #region Private Methods 
        private List<Vector2> GenerateInitialPositions(int particleCount)
        {
            return Enumerable.Range(0, particleCount)
                             .Select(x => new Vector2(RNG.RandomFloat(spaceConfig.MinimumInitialPositionX, spaceConfig.MaximumInitialPositionX),
                                                      RNG.RandomFloat(spaceConfig.MinimumInitialPositionY, spaceConfig.MaximumInitialPositionY)))
                             .ToList();
        }

        private int _particleTypeId;
        private List<ParticleType> GenerateParticleTypes(int particleTypeCount)
        {
            var _colors = new List<Color>() { Color.White, Color.Red, Color.Blue, Color.Green, Color.Magenta, Color.Yellow };

            var result = new List<ParticleType>();
            var interactions = GenerateParticleInteractions(particleTypeCount);

            for (int i = 0; i < particleTypeCount; i++)
            {
                var particleType = new ParticleType(++_particleTypeId - 1, interactions[i], _colors[i]); // Create particle type with it's interactions
                result.Add(particleType);
            }

            return result;
        }

        private List<Dictionary<int, float>> GenerateParticleInteractions(int particleTypeCount)
        {
            var result = new List<Dictionary<int, float>>();
            
            for (int i = 0; i < particleTypeCount; i++) // Loop every particle type
            {
                var interactions = new Dictionary<int, float>();
                for (int j = 0; j < particleTypeCount; j++) // For every particle type, it has interaction with every other particle.
                {
                    var randomInteraction = RNG.RandomFloat(spaceConfig.MinimumParticleInteraction, spaceConfig.MaximumParticleInteraction);
                    interactions.Add(j, randomInteraction);
                }

                result.Add(interactions);
            }

            return result;
        }
        #endregion
    }
}
