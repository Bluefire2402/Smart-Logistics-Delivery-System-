using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    public class Loader : Worker
    {
        private double maxLiftWeiht;

        public Loader(int experienceYears, int tasksCompleted, bool isAvailable, double maxLiftWeight) : base(experienceYears, tasksCompleted, isAvailable)
        {
            this.maxLiftWeiht = maxLiftWeight;
        }

        public override void PerformTask()
        {
            throw new NotImplementedException();
        }
    }
}
