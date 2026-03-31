using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;

namespace Project
{
    public abstract class Vehicle : Entity
    {
        private double speed;
        private double maxCapacity;
        private double currentLoad;
        private bool isAvailable;

        public Vehicle()
        {
        }
        public Vehicle(double speed, double maxCapacity, double currentLoad, bool isAvailable) : base()
        {
            this.speed = speed;
            this.maxCapacity = maxCapacity;
            this.currentLoad = currentLoad;
            this.isAvailable = isAvailable;
        }
        public double GetMaxCapacity()
        {
            return this.maxCapacity;
        }
        public bool IsAvailable()
        {
            return this.isAvailable;
        }
        public void SetCapicity(double capacity)
        {
            if (capacity <= 0)
            {
                throw new HandleException.InvalidCapacity();
            }
            this.maxCapacity = capacity;
        }
        public double GetCurrentLoad()
        {
            return this.currentLoad;
        }
        public void SetCurrentLoad(double load)
        {
            if (load < 0 || load > maxCapacity)
            {
                throw new HandleException.InvalidLoad();
            }
            this.currentLoad = load;
        }
        public double GetRemainingCapicity()
        {
            return maxCapacity - currentLoad;
        }
        public virtual double CalculateEfficiency()
        {
            if (currentLoad == 0)
            {
                return speed;
            }
            return speed / currentLoad;
        }
        public abstract void Deliver(List<Package> packages);
        public override void Display()
        {
            Console.WriteLine($"ID: {id}Vehicle Name: {name}, Speed: {speed}, Max Capicity: {maxCapacity}, Current Load: {currentLoad}, Is Available: {isAvailable}");
        }
    }
}
