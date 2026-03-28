using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;

namespace Project
{
    public class Van : Vehicle
    {
        private bool isElectric;

        public Van()
        {
        }

        public Van(double speed, double maxCapacity, double currentLoad, bool isAvailable, bool isElectric) : base(speed, maxCapacity, currentLoad, isAvailable)
        {
            this.isElectric = isElectric;
        }
        public override void Deliver(List<Package> packages)
        {
            throw new HandleException.InvalidPackageException();
        }
    }
}
