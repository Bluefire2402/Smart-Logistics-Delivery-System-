using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smart_Logistics___Delivery_System;
using SmartLogisticsDeliverySystem;

namespace SmartLogisticsDeliverySystem
{
    public class DeliverySystem
    {
        private List<Warehouse> warehouses;
        private List<Package> allPackages;
        public DeliverySystem()
        {
            this.warehouses = new List<Warehouse>();
            this.allPackages = new List<Package>();
        }

        public DeliverySystem(List<Warehouse> warehouses, List<Package> allPackages)
        {
            this.warehouses = warehouses;
            this.allPackages = allPackages;
        }
        public List<Package> GetAllPackages()
        {
            return this.allPackages;
        }
        public void AddWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                throw new HandleException.InvalidWarehouseException("Warehouse cannot be null");
            }
            warehouses.Add(warehouse);
        }
        public void AddPackage(Package p)
        {
            if (p == null)
            {
                throw new HandleException.InvalidPackageException("Package cannot be null");
            }
            allPackages.Add(p);
        }
        public Package SearchPackageById(int id)
        {
            foreach (var package in allPackages)
            {
                if (package.GetId() == id)
                {
                    return package;
                }
            }
            throw new HandleException.InvalidPackageException($"Package with ID {id} not found");
        }
        //sort by priority score
        public void SortPackages()
        {
            if (allPackages.Count == 0) return;

            for (int i = 0; i < allPackages.Count - 1; i++)
            {
                for (int j = 0; j < allPackages.Count - i - 1; j++)
                {
                    bool swap = false;
                    string statusJ = allPackages[j].GetStatus();
                    string statusNext = allPackages[j + 1].GetStatus();
                    //if one package is delivered and the other is pending, the delivered one should come after the pending one
                    if (statusJ.Equals("Delivered") && statusNext.Equals("Pending"))
                    {
                        swap = true;
                    }
                    //if both packages have the same status(even delivered), -> the one with the higher priority score should come first
                    else if (statusJ.Equals(statusNext))
                    {
                        if (allPackages[j].CalculatePriorityScore() < allPackages[j + 1].CalculatePriorityScore())
                        {
                            swap = true;
                        }
                    }

                    if (swap)
                    {
                        Package temp = allPackages[j];
                        allPackages[j] = allPackages[j + 1];
                        allPackages[j + 1] = temp;
                    }
                }
            }
        }
        public void ProcessDeliveries()
        {
            //each package -> check if delivered -> if not -> check each warehouse -> check available driver -> check available loader -> check manager if it has -> assign the package to the vehicle -> update status of the package to delivered
            int successCount = 0;
            //using queue for package waiting system
            CustomQueue<Package> waitingQueue = new CustomQueue<Package>();
            foreach (Package p in allPackages)
            {
                if (p.GetStatus().Equals("Pending"))
                {
                    waitingQueue.Enqueue(p);
                }
            }
            while (!waitingQueue.IsEmpty())
            {
                Package p = waitingQueue.Dequeue();
                bool assigned = false;
                if (p.GetStatus().Equals("Pending"))
                {
                    foreach (Warehouse w in warehouses)
                    {
                        try
                        {
                            Console.WriteLine($"Checking Package ID {p.GetId()} at Warehouse: {w.GetName()}");
                            Vehicle v = null;
                            try
                            {
                                v = w.FindBestVehicle(p);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine($"No suitable vehicle found in {w.GetName()} for this package type/weight.");
                                continue;
                            }
                            Worker driver = null;
                            try
                            {
                                driver = w.AsignWorker();
                            }
                            catch (Exception)
                            {
                                Console.WriteLine($"No available Driver in {w.GetName()}.");
                                continue;
                            }
                            Worker loader = null;
                            foreach (Worker worker in w.GetWorkers())
                            {
                                if (worker is Loader && worker.isWorkerAvailable())
                                {
                                    Loader tempLoader = (Loader)worker;
                                    if (tempLoader.GetMaxLiftWeight() >= p.GetWeight())
                                    {
                                        loader = worker;
                                        break;
                                    }
                                }
                            }
                            if (loader == null)
                            {
                                Console.WriteLine($"No Loader in {w.GetName()} is strong enough to lift {p.GetWeight()}kg.");
                                continue;
                            }
                            foreach (Worker worker in w.GetWorkers())
                            {
                                if (worker is Manager)
                                {
                                    worker.PerformTask();
                                    break;
                                }
                            }
                            loader.PerformTask();
                            driver.PerformTask();
                            List<Package> packageToDeliver = new List<Package>();
                            packageToDeliver.Add(p);
                            v.Deliver(packageToDeliver);
                            if (p.GetStatus().Equals("Delivered"))
                            {
                                Console.WriteLine($"Package {p.GetId()} delivered by {v.GetId()} & {driver.GetId()}.");
                                successCount++;
                                assigned = true;
                                break;
                            }

                        }
                        catch (Exception)
                        {
                            continue;
                        }

                    }
                }
                if (assigned == false)
                {
                    Console.WriteLine($"Cannot assign the package with id {p.GetId()}");
                }
                Console.WriteLine();

            }
            if (successCount == 0)
            {
                Console.WriteLine("No packages were successfully processed");
            }
            if (successCount > 0)
            {
                Console.WriteLine($"Successfully processed {successCount} packages");
            }
        }
        public void SimulateDay()
        {
            Console.WriteLine("Simulating a day in the delivery system");
            Console.WriteLine("Sorting packages by priority score");
            SortPackages();
            Console.WriteLine("Successfully sorted packages");
            Console.WriteLine("Processing deliveries for the day");
            ProcessDeliveries();
            Console.WriteLine("Finished processing deliverie for the day");
        }
        public List<Warehouse> GetWarehouses()
        {
            return warehouses;
        }
    }

}