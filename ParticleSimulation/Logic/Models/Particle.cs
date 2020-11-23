using Ara3D;
using ParticleSimulation.Logic.Models.ParticleData;

namespace ParticleSimulation.Logic.Models
{
    public class Particle
    {
        public int Id { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        
        public Vector2 ScheduledVelocity { get; set; }
        public Vector2 ScheduledPosition { get; set; }

        public int BatchId { get; set; }

        public IParticleData ParticleData { get; set; }


        public Particle(int id, Vector2 position, IParticleData particleData)
        {
            Id = id;
            Position = position;
            ScheduledPosition = position;
            ParticleData = particleData;
        }

        public T GetParticleData<T>() where T : IParticleData
        {
            try
            {
                return (T)ParticleData;
            } catch
            {
                return default(T);
            }
        }
    }
}
