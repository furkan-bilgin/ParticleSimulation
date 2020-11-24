using Ara3D;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;
using ParticleSimulation.Logic.Models.ParticleData;
using ParticleSimulation.Logic.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Controllers.ParticleControllers
{
    public class GravityParticleController : ParticleController
    {
        public GravityParticleController(SpaceConfig spaceConfig) : base(spaceConfig)
        {
            this.spaceConfig = spaceConfig;
        }

        public override List<IParticleData> GenerateParticleData(int count)
        {
            var result = new List<IParticleData>();
            var gravityConfig = (GravityConfig)spaceConfig;

            for (int i = 0; i < count; i++)
            {
                var radius = RNG.RandomFloat(gravityConfig.MinimumMass, gravityConfig.MaximumMass);
                var data = new GravityParticleData(radius, radius, RNG.RandomColor()); // Create particle type with it's interactions
                result.Add(data);
            }

            return result;
        }

        public override Particle CreateParticle(IParticleData particleData, Vector2 startPosition)
        {
            var gravityConfig = (GravityConfig)spaceConfig;
            var particle = base.CreateParticle(particleData, startPosition);

            particle.Velocity = new Vector2(RNG.RandomFloat(gravityConfig.MinimumInitialVelocity, gravityConfig.MaximumInitialVelocity), RNG.RandomFloat(gravityConfig.MinimumInitialVelocity, gravityConfig.MaximumInitialVelocity));
            particle.ScheduledVelocity = particle.Velocity;

            return particle;
        }
    }
}
