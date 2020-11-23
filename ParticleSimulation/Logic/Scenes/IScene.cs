using ParticleSimulation.Logic.Controllers;
using ParticleSimulation.Logic.Models;
using ParticleSimulation.Logic.Models.Configs;

namespace ParticleSimulation.Logic.Scenes
{
    public interface IScene
    {
        SpaceConfig Config { get; set; }

        void Initialize(LogicController logicController, Space space);
    }
}
