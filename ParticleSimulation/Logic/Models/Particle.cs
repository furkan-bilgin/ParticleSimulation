using Ara3D;

namespace ParticleSimulation.Logic.Models
{
    public class Particle
    {
        public int Id { get; private set; }
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 ScheduledPosition { get; set; }
        public ParticleType ParticleType { get; private set; }


        public Particle(int id, ParticleType particleType, Vector2 position)
        {
            this.Id = id;
            this.Position = position;
            this.ScheduledPosition = position;
            this.ParticleType = particleType;
        }

        public Particle Clone()
        {
            return new Particle(Id, ParticleType, Position.Add(Vector2.Zero));
        }
    }
}
