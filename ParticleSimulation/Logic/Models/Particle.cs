using Ara3D;
using ParticleSimulation.Logic.Models.ParticleData;

namespace ParticleSimulation.Logic.Models
{
    public class Particle
    {
        public int Id { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        
        public Vector2 ScheduledPosition { get; set; }

        public int BatchId { get; set; }

        private IParticleData particleData { get; set; }


        public Particle(int id, Vector2 position, IParticleData particleData)
        {
            this.Id = id;
            this.Position = position;
            this.ScheduledPosition = position;
            this.particleData = particleData;
        }

        public T GetParticleData<T>() where T : IParticleData
        {
            return (T)particleData;
        }
    }
}
