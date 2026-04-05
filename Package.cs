using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLogisticsDeliverySystem

namespace SmartLogisticsDeliverySystem
{
    public class Package
    {
        private int id;
        private double weight;
        private int priorityLevel;
        private string destination;
        private string status;

        public double CalculatePriorityScore()
        {
            return weight * priorityLevel;
        }
        public Package(int id, double weight, int priorityLevel, string destination, string status)
        {
            if (id <= 0)
            {
                throw new HandleException.InvalidPackageException("ID must be positive");
            }
            if (weight <= 0)
            {
                throw new HandleException.InvalidPackageException("Weight must be positive");
            }
            if (priorityLevel < 1 || priorityLevel > 5)
            {
                throw new HandleException.InvalidPackageException("Priority level must be between 1 and 5");
            }
            if (string.IsNullOrEmpty(destination))
            {
                throw new HandleException.InvalidPackageException("Destination cannot be empty");
            }
            if (status != "Pending" && status != "Assigned" && status != "Delivered")
            {
                throw new HandleException.InvalidPackageException("Status must be 'Pending', 'Assigned', or 'Delivered'");
            }
            this.id = id;
            this.weight = weight;
            this.priorityLevel = priorityLevel;
            this.destination = destination;
            this.status = status;
        }
        public int GetId()
        {
            return this.id;
        }
        public double GetWeight()
        {
            return this.weight;
        }
        public string GetStatus()
        {
            return this.status;
        }

        public void UpdateStatus(string newStatus)
        {
            if (newStatus != "Pending" && newStatus != "Assigned" && newStatus != "Delivered")
            {
                throw new HandleException.InvalidStatus();
            }
            this.status = newStatus;
        }
        public bool IsHeavy()
        {
            if (weight > 23)
            {
                return true;
            }
            return false;
        }
        public bool IsMedium()
        {
            if (weight > 10 && weight <= 23)
            {
                return true;
            }
            return false;
        }
        public bool IsLight()
        {
            if (weight <= 10)
            {
                return true;
            }
            return false;
        }

    }
}
