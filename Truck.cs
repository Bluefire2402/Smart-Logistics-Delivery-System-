using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    public class Truck : Vehicle
    {
        private double fuelConsumption;
        public Truck(double speed, double maxCapacity, double currentLoad, bool isAvailable, double fuelConsumption) : base(speed, maxCapacity, currentLoad, isAvailable)
        {
            this.fuelConsumption = fuelConsumption;
        }
        public override void Deliver(List<Package> packages)
        {
            foreach (Package package in packages)
            {
                if (package != null)
                {
                    if (package.GetWeight() <= GetRemainingCapacity() && package.IsHeavy())
                    {
                        SetCurrentLoad(GetCurrentLoad() + package.GetWeight());
                        package.UpdateStatus("Delivered");
                    }

                }
            }
        }
        public override double CalculateEfficiency()
        {
            if (fuelConsumption <= 0)
            {
                throw new HandleException.InvalidFuelConsumption("Fuel consumption must be greater than zero");
            }
            return base.CalculateEfficiency() / fuelConsumption;
        }
    }
}
