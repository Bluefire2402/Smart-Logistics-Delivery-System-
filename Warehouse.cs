using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
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
        public Warehouse(string name)
        {
            this.name = name;
            this.packages = new List<Package>();
            this.vehicles = new List<Vehicle>();
            this.workers = new List<Worker>();
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
        //-> find when the packge is pending
        public Vehicle FindBestVehicle(Package package)
        {
            //Handle exceptions for null package, and package that is not pending
            if (package == null)
            {
                throw new HandleException.InvalidPackageException("Package cannot be null");
            }
            if (package.GetStatus().Equals("Assigned") || package.GetStatus().Equals("Delivered"))
            {
                throw new HandleException.InvalidPackageException("The package have already been assigned or delivered");

            }
            //Find best vehicle
            double bestEfficiency = -1;
            Vehicle bestVehicle = null;
            for (int i = 0; i < vehicles.Count; i++)
            {
                if (vehicles[i].IsAvailable() && vehicles[i].GetRemainingCapacity() >= package.GetWeight())
                {
                    // Check the weight of the package then check the type of the vehicle if it's suitable for the package
                    bool isSuitable = false;
                    if (package.IsHeavy() && vehicles[i] is Truck)
                    {
                        isSuitable = true;
                    }
                    else if (package.IsMedium() && vehicles[i] is Van)
                    {
                        isSuitable = true;
                    }
                    else if (package.IsLight() && vehicles[i] is Drone)
                    {
                        isSuitable = true;
                    }
                    if (isSuitable)
                    {
                        double efficiency = vehicles[i].CalculateEfficiency();
                        if (efficiency > bestEfficiency)
                        {
                            bestEfficiency = efficiency;
                            bestVehicle = vehicles[i];
                        }
                    }

                }
            }
            // If it's null -> no vehicle
            if (bestVehicle == null)
            {
                throw new HandleException.NoBestVehicleException("No available vehicle can accommodate the package");
            }
            return bestVehicle;
        }
        public Worker AsignWorker()
        {
            //find the list available worker --> if there is a manager -> find bestworker. If there is no manager -> assign the first available worker. If there is no available worker -> throw exception
            List<Worker> availableWorkers = new List<Worker>();
            Manager warehouseManager = null;
            for (int i = 0; i < workers.Count; i++)
            {
                if (workers[i].isWorkerAvailable())
                {
                    availableWorkers.Add(workers[i]);
                }
                if (workers[i] is Manager)
                {
                    warehouseManager = (Manager)workers[i];
                }
            }
            if (availableWorkers.Count() == 0)

            {
                throw new HandleException.NoAvailableWoker("No available worker found at the moment");
            }
            // If there is a manager -> find the best worker
            if (warehouseManager != null)
            {
                Worker bestDriver = warehouseManager.FindBestWorker(availableWorkers);
                if (bestDriver == null)
                {
                    throw new HandleException.NoAvailableWoker("No available Driver found in this warehouse.");
                }
                return bestDriver;
            }
            //if there is no manager -> assign the first available worker
            foreach (Worker w in availableWorkers)
            {
                if (w is Driver) return w;
            }

            throw new HandleException.NoAvailableWoker("No available Driver found in this warehouse.");

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
        public void AddVehicle(Vehicle v)
        {
            if (v == null)
            {
                throw new HandleException.InvalidVehicleException("Vehicle cannot be null");
            }

            vehicles.Add(v);
        }
        public string GetName()
        {
            return name;
        }
        public void AddWorker(Worker worker)
        {
            if (worker == null)
            {
                throw new HandleException.InvalidWorkerException("Worker cannot be null");
            }
            workers.Add(worker);
        }
        public List<Vehicle> GetVehicles()
        {
            return this.vehicles;
        }

        public List<Worker> GetWorkers()
        {
            return this.workers;
        }
    }
}