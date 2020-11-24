using ParticleSimulation.Logic.Models.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParticleSimulation.Logic.Controllers
{
    public class TaskController
    {
        private int taskCount;
        public TaskController(int taskCount) 
        {
            this.taskCount = taskCount;
        }

        public void RunSchedules(List<ParticleJobSchedule> schedules)
        {
            var jobs = new Dictionary<int, List<ParticleJobSchedule>>(); // Group jobs for tasks
            foreach (var schedule in schedules)
            {
                if (!jobs.ContainsKey(schedule.Particle.BatchId))
                    jobs.Add(schedule.Particle.BatchId, new List<ParticleJobSchedule>());

                jobs[schedule.Particle.BatchId].Add(schedule);
            }
            /*
            Parallel.ForEach(jobs, (job) =>
            {
                foreach (var schedule in job.Value)
                {
                    LogicController.Instance.PhysicsController.UpdateParticle(schedule);
                }
            });
            */
            
            var tasks = new List<Task>();
            
            for (int i = 0; i < taskCount; i++) // Start tasks
            {
                var list = jobs[i];
                var task = Task.Run(() => 
                {
                    foreach (var schedule in list)
                    {
                        LogicController.Instance.PhysicsController.UpdateParticle(schedule);
                    }    
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray()); // Wait for tasks
        }
    }
}
