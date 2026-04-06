using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    public class Driver : Worker
    {
        private string licenseType;

        public Driver(int experienceYears, int tasksCompleted, bool isAvailable, string licenseType) : base(experienceYears, tasksCompleted, isAvailable)
        {
            this.licenseType = licenseType;
        }

        public override void PerformTask()
        {
            throw new NotImplementedException();
        }
    }
}
