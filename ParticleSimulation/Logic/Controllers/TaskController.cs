using ParticleSimulation.Logic.Models.Physics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Controllers
{
    public class TaskController
    {
        public void RunSchedules(List<ParticleJobSchedule> schedules)
        {
            Parallel.ForEach(schedules, (schedule) =>
            {
                LogicController.Instance.PhysicsController.UpdateParticle(schedule);
            });
        }
    }
}
