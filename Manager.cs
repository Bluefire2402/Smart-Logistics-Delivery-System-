using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;

namespace Project
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
            Worker best = null;
            for (int i = 0; i < workers.Count; i++)
            {
                for (int j = 0; j < workers.Count; j++)
                {
                    if (workers[i].CalculatePerformance() > workers[j].CalculatePerformance())
                    {
                        best = workers[i];
                    }
                }
            }
            if (best == null)
            {
                throw new HandleException.NoBestWorkerException();
            }
            return best;
        }

    }
}
