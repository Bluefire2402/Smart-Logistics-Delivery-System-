using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;

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
            throw new NotImplementedException();
        }
        public Worker FindBestWorker(List<Worker> workers)
        {
            if (workers == null || workers.Count == 0)
            {
                throw new HandleException.InvalidWorkerException("Worker list cannot be null or empty");
            }
            Worker best = workers[0];
            for (int i = 1; i < workers.Count; i++)
            {
                if (workers[i].CalculatePerformance() > best.CalculatePerformance())
                {
                    best = workers[i];
                }
            }
            return best;
        }

    }
}
