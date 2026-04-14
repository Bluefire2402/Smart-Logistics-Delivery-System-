using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLogisticsDeliverySystem;

namespace SmartLogisticsDeliverySystem
{
    public abstract class Worker : Entity
    {
        private int experienceYears;
        private int tasksCompleted;
        private bool isAvailable;

        public Worker(int experienceYears, int tasksCompleted, bool isAvailable) : base()
        {
            this.experienceYears = experienceYears;
            this.tasksCompleted = tasksCompleted;
            this.isAvailable = isAvailable;
        }
        public bool isWorkerAvailable()
        {
            return this.isAvailable;
        }
        public void addTask()
        {
            tasksCompleted++;
        }
        public int GetExperienceYears()
        {
            return experienceYears;
        }
        public int GetTasksCompleted()
        {
            return tasksCompleted;
        }
        public virtual double CalculatePerformance()
        {
            if (tasksCompleted == 0) return 0;
            return (experienceYears / tasksCompleted) / 100;
        }
        public abstract void PerformTask();
        public override void Display()
        {
            Console.WriteLine($"ID: {id}Worker Name: {name}, Experience Years: {experienceYears}, Tasks Completed: {tasksCompleted}, Is Available: {isAvailable}");
        }
    }

}
