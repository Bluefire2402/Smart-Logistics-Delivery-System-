using Smart_Logistics___Delivery_System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                DeliverySystem system = new DeliverySystem();
                FileManager fileManager = new FileManager();
                CustomStack<string> UndoStack = new CustomStack<string>();
                bool running = true;
                while (running)
                {
                    Console.WriteLine("----Smart Logistic Delibery System----");
                    Console.WriteLine("1.Add entities");
                    Console.WriteLine("2.Assign deliveries");
                    Console.WriteLine("3.Sort Packages");
                    Console.WriteLine("4.Search Package");
                    Console.WriteLine("5.Run Simulation");
                    Console.WriteLine("6.Undo");
                    Console.WriteLine("7.Save Data");
                    Console.WriteLine("8.Load Data");
                    Console.WriteLine("0.Exit");
                    Console.Write("Your choose: ");
                    string choice = Console.ReadLine();

                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                bool Subrunning = true;

                                while (Subrunning)
                                {
                                    Console.WriteLine("---Add Entities---");
                                    Console.WriteLine("1.Add Package");
                                    Console.WriteLine("2.Add Warehouse");
                                    Console.WriteLine("3.Add Vehicle");
                                    Console.WriteLine("4.Add Worker");
                                    Console.Write("Your choose: ");
                                    string SubChoice = Console.ReadLine();
                                    switch (SubChoice)
                                    {
                                        case "1":
                                            Console.WriteLine("Add Package:");
                                            Console.Write("Enter ID: ");
                                            int id = int.Parse(Console.ReadLine());
                                            Console.Write("Weight: ");
                                            double weight = double.Parse(Console.ReadLine());
                                            Console.Write("Priority (from 1-5): ");
                                            int priority = int.Parse(Console.ReadLine());
                                            Console.Write("Destination: ");
                                            string destination = Console.ReadLine();
                                            Package p = new Package(id, weight, priority, destination, "Pending");
                                            system.AddPackage(p);
                                            UndoStack.Push("AddPackage");
                                            Console.WriteLine("The package was added");
                                            break;

                                        case "2":
                                            Console.Write("Enter warehouse name: ");
                                            string name = Console.ReadLine();
                                            Warehouse w = new Warehouse(name);
                                            system.AddWarehouse(w);
                                            UndoStack.Push("AddWarehouse");
                                            Console.WriteLine("Warehouse was added");
                                            break;

                                        case "3":
                                            Console.Write("Please, enter the Warhouse name to add a new vehicle: ");
                                            string warehouseName = Console.ReadLine();
                                            Warehouse SelectedWarehouse = null;
                                            foreach (var war in system.GetWarehouses())
                                            {
                                                if (war.GetName() == warehouseName)
                                                {
                                                    SelectedWarehouse = war;
                                                    break;
                                                }
                                            }
                                            if (SelectedWarehouse == null)
                                            {
                                                Console.WriteLine("Warehouse not found");
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("---Add Vehicle---"); //menu in the menu in the menu
                                                Console.WriteLine("1.Truck");
                                                Console.WriteLine("2.Van");
                                                Console.WriteLine("3.Drone");
                                                Console.Write("Choose type: ");
                                                string VehicleChoice = Console.ReadLine();
                                                if (!int.TryParse(VehicleChoice, out int vehicleOption) || vehicleOption < 1 || vehicleOption > 3)
                                                {
                                                    Console.WriteLine("Invalid option");
                                                    break;
                                                }
                                                Console.WriteLine("Enter Vehicle ID: ");
                                                int vehicleId = int.Parse(Console.ReadLine());
                                                Console.WriteLine("Enter the speed: ");
                                                double speed = double.Parse(Console.ReadLine());
                                                Console.Write("Capacity: ");
                                                double capacity = double.Parse(Console.ReadLine());
                                                switch (VehicleChoice)
                                                {
                                                    case "1":

                                                        Console.Write("Fuel Consumption: ");
                                                        double tFuel = double.Parse(Console.ReadLine());
                                                        Truck truck = new Truck(speed, capacity, 0, true, tFuel);
                                                        truck.SetId(vehicleId);
                                                        SelectedWarehouse.AddVehicle(truck);
                                                        UndoStack.Push("AddVehicle|" + warehouseName);
                                                        Console.WriteLine("Truck added!");
                                                        break;

                                                    case "2":
                                                        Console.Write("Is electric: (true or false)");
                                                        bool isElectric = bool.Parse(Console.ReadLine());
                                                        Van van = new Van(speed, capacity, 0, true, isElectric);
                                                        van.SetId(vehicleId);
                                                        SelectedWarehouse.AddVehicle(van);
                                                        UndoStack.Push("AddVehicle|" + warehouseName);
                                                        Console.WriteLine("Van added!");
                                                        break;

                                                    case "3":
                                                        Console.Write("Enter Max Distance: ");
                                                        double maxDistance = double.Parse(Console.ReadLine());
                                                        Drone drone = new Drone(speed, capacity, 0, true, maxDistance);
                                                        drone.SetId(vehicleId);
                                                        SelectedWarehouse.AddVehicle(drone);
                                                        UndoStack.Push("AddVehicle|" + warehouseName);
                                                        Console.WriteLine("Dron was added.");
                                                        break;

                                                    default:
                                                        Console.WriteLine("Invalid option");
                                                        break;
                                                }
                                            }
                                            break;
                                        case "4":
                                            {
                                                Console.Write("Please, enter the Warhouse name to add a new Worker: ");
                                                string wareHouseNameForWorker = Console.ReadLine();
                                                Warehouse selectedWarehouseForWorker = null;
                                                foreach (var war in system.GetWarehouses())
                                                {
                                                    if (war.GetName() == wareHouseNameForWorker)
                                                    {
                                                        selectedWarehouseForWorker = war;
                                                        break;
                                                    }
                                                }
                                                if (selectedWarehouseForWorker == null)
                                                {
                                                    Console.WriteLine("Warehouse not found");
                                                    break;
                                                }
                                                else
                                                {
                                                    Console.WriteLine("---Add Worker---");
                                                    Console.WriteLine("1.Driver\n2.Loader\n3.Manager");
                                                    Console.Write("Choose type: ");
                                                    string workerChoice = Console.ReadLine();
                                                    if (!int.TryParse(workerChoice, out int workerOption) || workerOption < 1 || workerOption > 3)
                                                    {
                                                        Console.WriteLine("Invalid option");
                                                        break;
                                                    }
                                                    Console.WriteLine("Enter Worker ID: ");
                                                    int workerId = int.Parse(Console.ReadLine());
                                                    Console.Write("Experience Years: ");
                                                    int exp = int.Parse(Console.ReadLine());
                                                    Console.Write("Task Completed: ");
                                                    int taskCompleted = int.Parse(Console.ReadLine());
                                                    switch (workerChoice)
                                                    {
                                                        case "1":
                                                            Console.Write("License Type: ");
                                                            string license = Console.ReadLine();
                                                            Driver driver = new Driver(exp, taskCompleted, true, license);
                                                            driver.SetId(workerId);
                                                            selectedWarehouseForWorker.AddWorker(driver);
                                                            UndoStack.Push("AddWorker|" + wareHouseNameForWorker);
                                                            Console.WriteLine("Driver added!");
                                                            break;
                                                        case "2":
                                                            Console.Write("Max Lift Weight: ");
                                                            double lift = double.Parse(Console.ReadLine());
                                                            Loader loader = new Loader(exp, taskCompleted, true, lift);
                                                            loader.SetId(workerId);
                                                            selectedWarehouseForWorker.AddWorker(loader);
                                                            UndoStack.Push("AddWorker|" + wareHouseNameForWorker);
                                                            Console.WriteLine("Loader added!");
                                                            break;
                                                        case "3":
                                                            Console.Write("Team Size: ");
                                                            int team = int.Parse(Console.ReadLine());
                                                            Manager manager = new Manager(exp, taskCompleted, true, team);
                                                            manager.SetId(workerId);
                                                            selectedWarehouseForWorker.AddWorker(manager);
                                                            UndoStack.Push("AddWorker|" + wareHouseNameForWorker);
                                                            Console.WriteLine("Manager added!");
                                                            break;
                                                        default:
                                                            Console.WriteLine("Invalid option");
                                                            break;
                                                    }
                                                }

                                            }
                                            break;
                                    }
                                    break;
                                }
                                break;
                            case "2":
                                try
                                {
                                    system.ProcessDeliveries();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error in assign vehicles" + ex.Message);
                                }
                                break;

                            case "3":
                                try
                                {
                                    system.SortPackages();
                                    if (system.GetAllPackages().Count == 0)
                                    {
                                        Console.WriteLine("No packages to sort");
                                        break;
                                    }
                                    Console.WriteLine("Packages were sorted by priority (1-5) and status");
                                    List<Package> sortedPackages = system.GetAllPackages();
                                    foreach (Package p in sortedPackages)
                                    {
                                        Console.WriteLine($"ID: {p.GetId()}, Destination: {p.GetDestination()}, Status: {p.GetStatus()}, Priority Score: {p.CalculatePriorityScore()}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error in sorting packages" + ex.Message);
                                }
                                break;
                            case "4":
                                try
                                {
                                    Console.Write("Enter package ID: ");
                                    int id = int.Parse(Console.ReadLine());
                                    Package p = system.SearchPackageById(id);

                                    Console.WriteLine("Package found:");
                                    Console.WriteLine($"ID: {p.GetId()}");
                                    Console.WriteLine($"Destination: {p.GetDestination()}");
                                    Console.WriteLine($"Status: {p.GetStatus()}");
                                    Console.WriteLine($"Priority Score: {p.CalculatePriorityScore()}");


                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error" + ex.Message);
                                }
                                break;
                            case "5":
                                try
                                {
                                    system.SimulateDay();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Simulation Error: " + ex.Message);
                                }
                                break;
                            case "6":
                                try
                                {
                                    //Find Action -> if it is vehicle or worker: find warehouse  -> remove | else if it is package or warehouse -> remove from the system 
                                    if (UndoStack.IsEmpty())
                                    {
                                        Console.WriteLine("Nothing to undo");
                                        break;
                                    }

                                    string lastAction = UndoStack.pop();
                                    if (lastAction.Contains("|"))
                                    {
                                        string[] parts = lastAction.Split('|');
                                        string actionType = parts[0];
                                        string warehouseName = parts[1];
                                        Warehouse w = null;
                                        foreach (var war in system.GetWarehouses())
                                        {
                                            if (war.GetName() == warehouseName)
                                            {
                                                w = war;
                                                break;
                                            }
                                        }
                                        if (w != null)
                                        {
                                            if (actionType == "AddVehicle")
                                            {
                                                if (w.GetVehicles() != null && w.GetVehicles().Count > 0)
                                                {
                                                    w.GetVehicles().RemoveAt(w.GetVehicles().Count - 1);
                                                    Console.WriteLine("Last vehicle removed from warehouse " + warehouseName);
                                                }
                                            }
                                            else if (actionType == "AddWorker")
                                            {
                                                if (w.GetWorkers() != null && w.GetWorkers().Count > 0)
                                                {
                                                    w.GetWorkers().RemoveAt(w.GetWorkers().Count - 1);
                                                    Console.WriteLine("Last worker removed from warehouse " + warehouseName);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (lastAction == "AddPackage")
                                        {
                                            if (system.GetAllPackages().Count > 0)
                                            {
                                                system.GetAllPackages().RemoveAt(system.GetAllPackages().Count - 1);
                                                Console.WriteLine("Last package removed.");
                                            }
                                        }
                                        else if (lastAction == "AddWarehouse")
                                        {
                                            if (system.GetWarehouses().Count > 0)
                                            {
                                                system.GetWarehouses().RemoveAt(system.GetWarehouses().Count - 1);
                                                Console.WriteLine("Last warehouse removed.");
                                            }
                                        }
                                    }
                                }



                                catch (Exception ex)
                                {
                                    Console.WriteLine("Undo error: " + ex.Message);
                                }
                                break;
                            case "7":
                                try
                                {
                                    Console.WriteLine("Saving data...");
                                    fileManager.SetPackages(system.GetAllPackages());
                                    fileManager.SetWarehouses(system.GetWarehouses());
                                    List<Vehicle> allVehicles = new List<Vehicle>();
                                    List<Worker> allWorkers = new List<Worker>();
                                    foreach (Warehouse w in system.GetWarehouses())
                                    {
                                        if (w.GetVehicles() != null)
                                        {
                                            foreach (Vehicle v in w.GetVehicles())
                                            {
                                                allVehicles.Add(v);
                                            }
                                        }
                                        if (w.GetWorkers() != null)
                                        {
                                            foreach (Worker worker in w.GetWorkers())
                                            {
                                                allWorkers.Add(worker);
                                            }
                                        }
                                    }
                                    fileManager.SetVehicles(allVehicles);
                                    fileManager.SetWorkers(allWorkers);
                                    fileManager.Save("data.txt");
                                    Console.WriteLine("Data saved successfully.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error" + ex.Message);
                                }
                                break;
                            case "8":
                                try
                                {
                                    Console.WriteLine("Loading data");
                                    fileManager.Load("data.txt");
                                    foreach (Package p in fileManager.GetPackages())
                                    {
                                        system.AddPackage(p);
                                    }
                                    foreach (Warehouse w in fileManager.GetWarehouses())
                                    {
                                        foreach (Warehouse existingWarehouse in system.GetWarehouses())
                                        {
                                            if (existingWarehouse.GetName() == w.GetName())
                                            {
                                                break;
                                            }
                                        }
                                        system.AddWarehouse(w);
                                    }
                                    Console.WriteLine("Enter the warehouse name to load vehicles and workers: ");
                                    string warehouseName = Console.ReadLine();
                                    bool isNewWarehouse = false;
                                    Warehouse targetWarehouse = null;

                                    foreach (var war in system.GetWarehouses())
                                    {
                                        if (war.GetName() == warehouseName)
                                        {
                                            targetWarehouse = war;
                                            break;
                                        }
                                    }

                                    if (targetWarehouse == null)
                                    {
                                        targetWarehouse = new Warehouse(warehouseName);
                                        isNewWarehouse = true;
                                    }

                                    foreach (Vehicle v in fileManager.GetVehicles())
                                    {
                                        targetWarehouse.AddVehicle(v);
                                    }
                                    foreach (Worker w in fileManager.GetWorkers())
                                    {
                                        targetWarehouse.AddWorker(w);
                                    }

                                    if (isNewWarehouse == true)
                                    {
                                        system.AddWarehouse(targetWarehouse);
                                    }
                                    Console.WriteLine("Data loaded successfully.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error loading data: " + ex.Message);
                                }
                                break;
                            case "9":

                            case "0":
                                Console.WriteLine("Thanks for using");
                                running = false;
                                break;
                            default:
                                Console.WriteLine();
                                break;
                        }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }

                }
            }
        }
    }
}
