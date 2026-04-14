using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLogisticsDeliverySystem;

namespace SmartLogisticsDeliverySystem

{
    public class Manager : Worker
    {
        int teamSize;

        public Manager(int experienceYears, int tasksCompleted, bool isAvailable, int teamSize) : base(experienceYears, tasksCompleted, isAvailable)
        {
            this.teamSize = teamSize;
        }
        public override void PerformTask()
        {
            Console.WriteLine("manager is organizing workers");
            addTask();
        }
        public int GetTeamSize()
        {
            return teamSize;
        }
        public Worker FindBestWorker(List<Worker> workers)
        {
            //find best driver to assign
            if (workers == null || workers.Count == 0)
            {
                throw new HandleException.InvalidWorkerException("Worker list cannot be null or empty");
            }
            Worker best = null;
            double bestPerformance = double.MinValue;
            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i] is Driver)
                {
                    if (workers[i].CalculatePerformance() > bestPerformance)
                    {
                        best = workers[i];
                        bestPerformance = workers[i].CalculatePerformance();
                    }
                }

            }
            return best;
        }

    }
}
