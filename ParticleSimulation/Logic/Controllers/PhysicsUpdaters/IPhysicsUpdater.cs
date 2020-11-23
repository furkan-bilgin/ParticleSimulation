using ParticleSimulation.Logic.Models.Physics;

namespace ParticleSimulation.Logic.Controllers.PhysicsUpdaters
{
    public interface IPhysicsUpdater
    {
        void UpdatePhysics(ParticleJobSchedule schedule);
        void CacheParticleData(SpaceSnapshot spaceSnapshot);
    }
}
