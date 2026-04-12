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
                FileManager FileManager = new FileManager();
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
                                            Console.Write("Please, enter the Warhouse name: ");
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
                                            Console.WriteLine("---Add Vehicle---"); //menu in the menu in the menu
                                            Console.WriteLine("1.Truck");
                                            Console.WriteLine("2.Van");
                                            Console.WriteLine("3.Drone");
                                            Console.Write("Choose type: ");

                                            string VehicleChoice = Console.ReadLine();
                                            switch (VehicleChoice)
                                            {
                                                case "1":
                                                    Console.Write("Capacity: ");
                                                    double tCapacity = double.Parse(Console.ReadLine());
                                                    Console.Write("Fuel Consumption: ");
                                                    double tFuel = double.Parse(Console.ReadLine());
                                                    Truck truck = new Truck(50, tCapacity, 0, true, tFuel);
                                                    SelectedWarehouse.AddVehicle(truck);
                                                    UndoStack.Push("AddTruck");
                                                    Console.WriteLine("Truck added!");
                                                    break;

                                                case "2":
                                                    Console.Write("Capacity: ");
                                                    double vCapacity = double.Parse(Console.ReadLine());
                                                    Console.Write("Fuel Consumption: ");
                                                    double vFuel = double.Parse(Console.ReadLine());
                                                    Van van = new Van(50, vCapacity, 0, true, true);
                                                    SelectedWarehouse.AddVehicle(van);
                                                    UndoStack.Push("AddVan");
                                                    Console.WriteLine("Van added!");
                                                    break;

                                                case "3":
                                                    Console.Write("Capacity: ");
                                                    double dCapacity = double.Parse(Console.ReadLine());
                                                    Console.Write("Battery Life: ");
                                                    double battery = double.Parse(Console.ReadLine());
                                                    Drone drone = new Drone(50, dCapacity, 0, true, battery);
                                                    SelectedWarehouse.AddVehicle(drone);
                                                    UndoStack.Push("AddDrone");
                                                    Console.WriteLine("Dron was added.");
                                                    break;

                                                default:
                                                    Console.WriteLine("Invalid option");
                                                    break;
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
                                    Console.WriteLine("Delivery was assign.");
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
                                    Console.WriteLine("Packages were sorted by priority (1-5)");
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

                                break;
                            case "6":
                                try
                                {
                                    if (UndoStack.IsEmpty())
                                    {
                                        Console.WriteLine("Nothing to undo");
                                        break;
                                    }

                                    string lastAction = UndoStack.pop();
                                    switch (lastAction)
                                    {
                                        case "AddPackage":
                                            if (system.GetAllPackages().Count > 0)
                                            {
                                                system.GetAllPackages().RemoveAt(system.GetAllPackages().Count - 1);
                                                Console.WriteLine("Last package removed.");
                                            }
                                            break;
                                        case "AddWarehouse":
                                            if (system.GetWarehouses().Count > 0)
                                            {
                                                system.GetWarehouses().RemoveAt(system.GetWarehouses().Count - 1);
                                                Console.WriteLine("Last warehouse removed.");
                                            }
                                            break;
                                        case "AddTruck":
                                        case "AddVan":
                                        case "AddDrone":
                                            Console.WriteLine("Last vehicle undo not fully implemented.");
                                            break;
                                        default:
                                            Console.WriteLine("Unknown action.");
                                            break;
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

                                    FileManager.SetPackages(system.GetAllPackages());
                                    FileManager.Save("data.txt");
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
                                    FileManager.Load("data.txt");
                                    foreach (Package p in FileManager.GetPackages())
                                    {
                                        system.AddPackage(p);
                                    }
                                    Console.WriteLine("Data loaded successfully.");
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error loading data: " + ex.Message);
                                }
                                break;
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
