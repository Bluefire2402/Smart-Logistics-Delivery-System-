using Smart_Logistics___Delivery_System;
using System;
using System.Collections.Generic;
using System.Linq;
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

                                        foreach (var w in system.GetWarehouses())
                                        {
                                            if (w.GetName() == warehouseName)
                                            {
                                                SelectedWarehouse = w;
                                                break;
                                            }
                                        }

                                        if (SelectedWarehouse == null)
                                        {
                                            Console.WriteLine("Warehouse not found!");
                                            break;
                                        }
                                        Console.WriteLine("---Add Vehicle---");
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

                                                Truck truck = new Truck(tCapacity, tFuel);

                                                SelectedWarehouse.AddVehicle(truck);

                                                UndoStack.Push("AddTruck");
                                                Console.WriteLine("Truck added!");
                                                break;

                                            case "2":
                                                Console.Write("Capacity: ");
                                                double vCapacity = double.Parse(Console.ReadLine());

                                                Console.Write("Fuel Consumption: ");
                                                double vFuel = double.Parse(Console.ReadLine());

                                                Van van = new Van(vCapacity, vFuel);

                                                SelectedWarehouse.AddVehicle(van);

                                                UndoStack.Push("AddVan");
                                                Console.WriteLine("Van added!");
                                                break;

                                            case "3":
                                                Console.Write("Capacity: ");
                                                double dCapacity = double.Parse(Console.ReadLine());

                                                Console.Write("Battery Life: ");
                                                double battery = double.Parse(Console.ReadLine());

                                                Drone drone = new Drone(dCapacity, battery);

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
                     
                            case "2":

                                break;
                            case "3":

                                break;
                            case "4":

                                break;
                            case "5":

                                break;
                            case "6":

                                break;
                            case "7":

                                break;
                            case "8":

                                break;
                            case "0":

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
