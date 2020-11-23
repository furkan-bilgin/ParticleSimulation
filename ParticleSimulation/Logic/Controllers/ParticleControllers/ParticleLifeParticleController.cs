using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.ParticleData;
using ParticleSimulation.Logic.Utils;
using SFML.Graphics;
using System.Collections.Generic;

namespace ParticleSimulation.Logic.Controllers.ParticleControllers
{
    public class ParticleLifeParticleController : ParticleController
    {
        public ParticleLifeParticleController(SpaceConfig spaceConfig) : base(spaceConfig)
        {
            this.spaceConfig = spaceConfig;
        }
        
        private int _particleTypeId;
        public override List<IParticleData> GenerateParticleData(int count)
        {
            var result = new List<IParticleData>();
            var interactions = GenerateParticleInteractions(count);

            for (int i = 0; i < count; i++)
            {
                var data = new ParticleLifeParticleData(new ParticleType(++_particleTypeId - 1, interactions[i]), RNG.RandomColor()); // Create particle type with it's interactions
                result.Add(data);
            }

            return result;
        }

        private List<Dictionary<int, ParticleInteraction>> GenerateParticleInteractions(int particleTypeCount)
        {
            var result = new List<Dictionary<int, ParticleInteraction>>();
            var particleLifeConfig = (ParticleLifeConfig)spaceConfig;

            for (int i = 0; i < particleTypeCount; i++) // Loop every particle type
            {
                var interactions = new Dictionary<int, ParticleInteraction>();

                for (int j = 0; j < particleTypeCount; j++) // For every particle type, it has interaction with every other particle.
                {
                    var interaction = new ParticleInteraction()
                    {
                        GraphDots = new List<Vector2>()
                    };

                    for (int k = 0; k < particleLifeConfig.MinimumParticleInteractions.Count; k++) // Generate all graph dots...
                    {
                        var xVal = RNG.RandomFloat(particleLifeConfig.MinimumParticleInteractionDistances[k], particleLifeConfig.MaximumParticleInteractionDistances[k]);
                        var yVal = RNG.RandomFloat(particleLifeConfig.MinimumParticleInteractions[k], particleLifeConfig.MaximumParticleInteractions[k]);

                        interaction.GraphDots.Add(new Vector2(xVal, yVal));
                    }

                    interactions.Add(j, interaction); // Add to interactions
                }

                result.Add(interactions);
            }

            return result;
        }
    }
}
