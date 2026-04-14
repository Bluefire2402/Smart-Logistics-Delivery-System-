using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    public class Loader : Worker
    {
        private double maxLiftWeight;

        public Loader(int experienceYears, int tasksCompleted, bool isAvailable, double maxLiftWeight) : base(experienceYears, tasksCompleted, isAvailable)
        {
            this.maxLiftWeight = maxLiftWeight;
        }
        public double GetMaxLiftWeight()
        {
            return this.maxLiftWeight;
        }
        public override void PerformTask()
        {
            Console.WriteLine("The loader is working");
            addTask();
        }
    }
}
