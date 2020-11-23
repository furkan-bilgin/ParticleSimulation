using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.ParticleData;
using ParticleSimulation.Logic.Utils;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace ParticleSimulation.Logic.Controllers.ParticleControllers
{
    public class ParticleController
    {
        protected SpaceConfig spaceConfig;

        public ParticleController(SpaceConfig spaceConfig)
        {
            this.spaceConfig = spaceConfig;
        }

        private int _particleId;
        /// <summary>
        /// Generate a particle with given particletype and startPosition
        /// </summary>
        public virtual Particle CreateParticle(IParticleData particleData, Vector2 startPosition)
        {
            var particle = new Particle(++_particleId, startPosition, particleData);
            particle.BatchId = RNG.RandomInt(0, spaceConfig.BatchCount);
            return particle;
        }

        /// <summary>
        /// Generate initial particles
        /// </summary>
        public virtual List<Particle> GenerateInitialParticles()
        {
            List<IParticleData> particleDatas = null;

            if (spaceConfig.AutoGenerateParticleData)
                particleDatas = GenerateParticleData(spaceConfig.GeneratedParticleDataCount);
            else
                particleDatas = new List<IParticleData>() { GetDefaultParticleData() };


            var initialPositions = GenerateInitialPositions(spaceConfig.ParticleCount);

            return Enumerable.Range(0, spaceConfig.ParticleCount)
                             .Select(x => CreateParticle(RNG.RandomElement(particleDatas), initialPositions[x]))
                             .ToList();
        }

        public virtual List<IParticleData> GenerateParticleData(int count)
        {
            return null;
        }

        public virtual IParticleData GetDefaultParticleData()
        {
            return null;
        }

        #region Private Methods 
        private List<Vector2> GenerateInitialPositions(int particleCount)
        {
            return Enumerable.Range(0, particleCount)
                             .Select(x => new Vector2(RNG.RandomFloat(spaceConfig.MinimumInitialPositionX, spaceConfig.MaximumInitialPositionX),
                                                      RNG.RandomFloat(spaceConfig.MinimumInitialPositionY, spaceConfig.MaximumInitialPositionY)))
                             .ToList();
        }
        #endregion
    }
}
