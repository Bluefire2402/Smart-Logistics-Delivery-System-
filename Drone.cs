using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;

namespace SmartLogisticsDeliverySystem
{
    public class Drone : Vehicle
    {
        private double maxDistance;

        public Drone()
        {
        }

        public Drone(double speed, double maxCapacity, double currentLoad, bool isAvailable, double maxDistance) : base(speed, maxCapacity, currentLoad, isAvailable)
        {
            this.maxDistance = maxDistance;
        }
        public override double CalculateEfficiency()
        {
            return base.CalculateEfficiency() * maxDistance;
        }
        public override void Deliver(List<Package> packages)
        {
            foreach (Package package in packages)
            {
                if (package != null && package.GetWeight() < GetRemainingCapicity() && package.IsLight())
                {
                    SetCurrentLoad(GetCurrentLoad() + package.GetWeight());
                    package.UpdateStatus("Delivered");

                }
            }
        }

    }
}
