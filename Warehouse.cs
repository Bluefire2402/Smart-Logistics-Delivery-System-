using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;

namespace Project
{
    public class Warehouse
    {
        private string name;
        private List<Package> packages;
        private List<Vehicle> vehicles;
        private List<Worker> workers;
        public Warehouse()
        {
            packages = new List<Package>();
            vehicles = new List<Vehicle>();
            workers = new List<Worker>();

        }

        public Warehouse(string name, List<Package> packages, List<Vehicle> vehicles, List<Worker> workers)
        {
            this.name = name;
            this.packages = packages;
            this.vehicles = vehicles;
            this.workers = workers;
        }
        public void AddPackage(Package package)
        {
            if (package == null)
            {
                throw new HandleException.InvalidPackageException("Package cannot be null");
            }
            packages.Add(package);
        }
        public void RemovePackage(int packageId)
        {
            bool found = false;
            for (int i = 0; i < packages.Count; i++)
            {
                if (packages[i].GetId() == packageId)
                {
                    packages.RemoveAt(i);
                    return;
                }
            }
            if (!found)
            {
                Console.WriteLine($"Package with ID {packageId} not found");
            }

        }
        public Vehicle FindBestVehicle(Package package)
        {
            if (package == null)
            {
                throw new HandleException.InvalidPackageException("Package cannot be null");
            }
            double bestEfficiency = 0;
            Vehicle bestVehicle = null;
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].IsAvailable() && vehicles[i].GetRemainingCapicity() >= package.GetWeight())
                {
                    double efficiency = vehicles[i].CalculateEfficiency();
                    if (efficiency > bestEfficiency)
                    {
                        bestEfficiency = efficiency;
                        bestVehicle = vehicles[i];
                    }
                }
            }
            if (bestVehicle == null)
            {
                throw new HandleException.NoBestVehicleException("No available vehicle can accommodate the package");
            }
            return bestVehicle;
        }
        public Worker AsignWorker()
        {
            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].isWorkerAvailable())
                    return workers[i];
            }
            throw new Exception("No available worker found");
        }
        public List<Package> GetPendingPackages()
        {
            List<Package> packages = new List<Package>();
            for (int i = 0; i < this.packages.Count; i++)
            {
                if (this.packages[i].GetStatus().Equals("Pending"))
                {
                    packages.Add(this.packages[i]);
                }
            }
            if (packages.Count == 0)
            {
                throw new HandleException.NoPendingPackagesException("No pending packages found");
            }
            return packages;
        }
    }
}
