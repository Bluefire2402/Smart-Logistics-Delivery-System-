using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject;

namespace SmartLogisticsDeliverySystem
{
    public class DeliverySystem
    {
        private List<Warehouse> warehouses;
        private List<Package> allPackages;

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
            for (int i = 0; i < allPackages.Count - 1; i++)
            {
                for (int j = 0; j < allPackages.Count - i - 1; j++)
                {
                    if (allPackages[i].CalculatePriorityScore() < allPackages[j].CalculatePriorityScore())
                    {
                        Package temp = allPackages[i];
                        allPackages[i] = allPackages[j];
                        allPackages[j] = temp;
                    }
                }
            }
        }
        public void ProcessDeliveries()
        {
            // for each package -> for warehouse -> find best vehicle in the warehouse -> assign worker -> update package status to assigned
            foreach (Package p in allPackages)
            {
                bool assigned = false;
                if (p.GetStatus().Equals("Pending"))
                {
                    foreach (Warehouse w in warehouses)
                    {
                        //If there is no vehicle (Function FindBestVehicle will throw the exception) -> catch exception and continue to the next warehouse  
                        try
                        {
                            Vehicle v = w.FindBestVehicle(p);
                            Worker worker = w.AsignWorker();
                            if (v != null && worker != null)
                            {
                                p.UpdateStatus("Assigned");
                                worker.addTask();
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
            }
        }
        public void SimulateDay()
        {
            throw new NotImplementedException();
        }
    }

}