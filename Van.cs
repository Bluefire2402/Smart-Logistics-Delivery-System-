using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    public class Van : Vehicle
    {
        private bool isElectric;
        public Van(double speed, double maxCapacity, double currentLoad, bool isAvailable, bool isElectric) : base(speed, maxCapacity, currentLoad, isAvailable)
        {
            this.isElectric = isElectric;
        }
        public override void Deliver(List<Package> packages)
        { 
            foreach (Package package in packages)
            {
                if (package != null)
                {
                    if (package.GetWeight() <= GetRemainingCapacity() && package.IsMedium())
                    {
                        SetCurrentLoad(GetCurrentLoad() + package.GetWeight());
                        package.UpdateStatus("Delivered");
                    }
                }
            }
        }
    }
}
