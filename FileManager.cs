using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartLogisticsDeliverySystem;

namespace SmartLogisticsDeliverySystem
{

    internal class FileManager : IFileManager, ISortable
    {
        private List<Package> loadedPackages = new List<Package>();
        private List<Vehicle> loadedVehicles = new List<Vehicle>();
        private List<Worker> loadedWorkers = new List<Worker>();
        private List<Warehouse> loadedWarehouses = new List<Warehouse>();
        public void Load(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("The file was not found");
                return;
            }
            loadedPackages.Clear();
            loadedVehicles.Clear();
            loadedWorkers.Clear();
            loadedWarehouses.Clear();

            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split('|');

                    if (parts[0] == "PACKAGE")
                    {
                        int id = int.Parse(parts[1]);
                        double weight = double.Parse(parts[2]);
                        int priority = int.Parse(parts[3]);
                        string destination = parts[4];
                        string status = parts[5];
                        Package p = new Package(id, weight, priority, destination, status);
                        loadedPackages.Add(p);
                    }
                    else if (parts[0] == "VEHICLE")
                    {
                        int id = int.Parse(parts[1]);
                        double maxCapacity = double.Parse(parts[3]);
                        bool isAvailable = bool.Parse(parts[4]);

                        if (parts[2] == "TRUCK")
                        {
                            double fuel = double.Parse(parts[5]);
                            // Constructor: speed, maxCapacity, currentLoad, isAvailable, fuelConsumption
                            Truck t = new Truck(50, maxCapacity, 0, isAvailable, fuel);
                            t.SetId(id);
                            loadedVehicles.Add(t);
                        }
                        else if (parts[2] == "VAN")
                        {
                            bool isElectric = bool.Parse(parts[5]);
                            // Constructor: speed, maxCapacity, currentLoad, isAvailable, isElectric
                            Van v = new Van(50, maxCapacity, 0, isAvailable, isElectric);
                            v.SetId(id);
                            loadedVehicles.Add(v);
                        }
                        else if (parts[2] == "DRONE")
                        {
                            double maxDistance = double.Parse(parts[5]);
                            // Constructor: speed, maxCapacity, currentLoad, isAvailable, maxDistance
                            Drone d = new Drone(50, maxCapacity, 0, isAvailable, maxDistance);
                            d.SetId(id);
                            loadedVehicles.Add(d);
                        }
                    }
                    else if (parts[0] == "WORKER")
                    {
                        int id = int.Parse(parts[1]);
                        int exp = int.Parse(parts[3]);
                        int tasksCompleted = int.Parse(parts[4]);
                        bool isAvailable = bool.Parse(parts[5]);

                        if (parts[2] == "DRIVER")
                        {
                            string license = parts[6];
                            // Constructor: experienceYears, tasksCompleted, isAvailable, licenseType
                            Driver d = new Driver(exp, tasksCompleted, isAvailable, license);
                            d.SetId(id);
                            loadedWorkers.Add(d);
                        }
                        else if (parts[2] == "LOADER")
                        {
                            double liftWeight = double.Parse(parts[6]);
                            // Constructor: experienceYears, tasksCompleted, isAvailable, maxLiftWeight
                            Loader l = new Loader(exp, tasksCompleted, isAvailable, liftWeight);
                            l.SetId(id);
                            loadedWorkers.Add(l);
                        }
                        else if (parts[2] == "MANAGER")
                        {
                            int teamSize = int.Parse(parts[6]);
                            // Constructor: experienceYears, tasksCompleted, isAvailable, teamSize
                            Manager m = new Manager(exp, tasksCompleted, isAvailable, teamSize);
                            m.SetId(id);
                            loadedWorkers.Add(m);
                        }

                    }
                    else if (parts[0] == "WAREHOUSE")
                    {
                        string name = parts[1];
                        Warehouse w = new Warehouse(name);
                        loadedWarehouses.Add(w);
                    }
                }

                Console.WriteLine("The data was loaded.");
            }
        }

        public void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Package p in loadedPackages)
                {
                    writer.WriteLine($"PACKAGE|{p.GetId()}|{p.GetWeight()}|{p.GetPriorityLevel()}|{p.GetDestination()}|{p.GetStatus()}");
                }

                foreach (Vehicle v in loadedVehicles)
                {
                    // Format: VEHICLE|ID|TYPE|MAX_CAPACITY|IS_AVAILABLE|SPECIAL FEATURES
                    if (v is Truck t)
                    {
                        writer.WriteLine($"VEHICLE|{t.GetId()}|TRUCK|{t.GetMaxCapacity()}|{t.IsAvailable()}|{t.GetFuelConsumption()}");
                    }
                    else if (v is Van van)
                    {
                        writer.WriteLine($"VEHICLE|{van.GetId()}|VAN|{van.GetMaxCapacity()}|{van.IsAvailable()}|{van.IsElectric()}");
                    }
                    else if (v is Drone d)
                    {
                        writer.WriteLine($"VEHICLE|{d.GetId()}|DRONE|{d.GetMaxCapacity()}|{d.IsAvailable()}|{d.GetMaxDistance()}");
                    }
                }

                foreach (Worker w in loadedWorkers)
                {
                    // Format: WORKER | id | LOẠI | Số_năm_KN | Số_Task_đã_làm | IsAvailable | Đặc_điểm_riêng
                    if (w is Driver d)
                    {
                        writer.WriteLine($"WORKER|{d.GetId()}|DRIVER|{d.GetExperienceYears()}|{d.GetTasksCompleted()}|{d.isWorkerAvailable()}|{d.GetLicenseType()}");
                    }
                    else if (w is Loader l)
                    {
                        writer.WriteLine($"WORKER|{l.GetId()}|LOADER|{l.GetExperienceYears()}|{l.GetTasksCompleted()}|{l.isWorkerAvailable()}|{l.GetMaxLiftWeight()}");
                    }
                    else if (w is Manager m)
                    {
                        writer.WriteLine($"WORKER|{m.GetId()}|MANAGER|{m.GetExperienceYears()}|{m.GetTasksCompleted()}|{m.isWorkerAvailable()}|{m.GetTeamSize()}");
                    }

                }
                //Format: WAREHOUSE|NAME
                foreach (Warehouse w in loadedWarehouses)
                {
                    writer.WriteLine($"WAREHOUSE|{w.GetName()}");
                }
            }
            Console.WriteLine("The data was saved.");
        }
        public void SetPackages(List<Package> packages)
        {
            loadedPackages = packages;
        }
        public List<Package> GetPackages()
        {
            return loadedPackages;
        }
        public void SetVehicles(List<Vehicle> vehicles)
        {
            loadedVehicles = vehicles;
        }
        public List<Vehicle> GetVehicles()
        {
            return loadedVehicles;
        }

        public void SetWorkers(List<Worker> workers)
        {
            loadedWorkers = workers;
        }
        public List<Worker> GetWorkers()
        {
            return loadedWorkers;
        }
        public void SetWarehouses(List<Warehouse> warehouses)
        {
            loadedWarehouses = warehouses;
        }
        public List<Warehouse> GetWarehouses()
        {
            return loadedWarehouses;
        }
        public void Sort()
        {
            int n = loadedPackages.Count;

            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    double current = loadedPackages[j].CalculatePriorityScore();
                    double next = loadedPackages[j + 1].CalculatePriorityScore();
                    if (current < next)
                    {
                        Package temp = loadedPackages[j];
                        loadedPackages[j] = loadedPackages[j + 1];
                        loadedPackages[j + 1] = temp;
                    }
                }
            }
        }
    }
}

