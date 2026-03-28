using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Truck : Vehicle
    {
        private double fuelConsumption;
        public override void Deliver(List<Package> packages)
        {
            throw new NotImplementedException();
        }
        public override double CalculateEfficiency()
        {
            return base.CalculateEfficiency() / fuelConsumption;
        }
    }
}
