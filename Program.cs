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
                FileManager fileManager = new FileManager();
                CustomStack<string> undoStack = new CustomStack<string>();

                bool running = true;

                while (running)
                {
                    Console.WriteLine("----Smart Logistic Delibery System----");
                    Console.WriteLine("1.Add Package");
                    Console.WriteLine("2.Assign Deliveries");
                    Console.WriteLine("3.Sort Packages");
                    Console.WriteLine("4.Search Package");
                    Console.WriteLine("5.Run Simulation");
                    Console.WriteLine("6.Undo Last Action");
                    Console.WriteLine("7.Save Data");
                    Console.WriteLine("8.Load Data");
                    Console.WriteLine("0.Exit");
                    Console.Write("Your option: ");
                    string choice = Console.ReadLine();

                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                Console.Write("Enter ID: ");
                                int id = int.Parse(Console.ReadLine());

                                Console.Write("Weight: ");
                                double weight = double.Parse(Console.ReadLine());

                                Console.Write("Priority (1-5): ");
                                int priority = int.Parse(Console.ReadLine());

                                Console.Write("Destination: ");
                                string destination = Console.ReadLine();

                                Package p = new Package(id, weight, priority, destination, "Pending");
                                system.AddPackage(p);

                                undoStack.Push("AddPackage");
                                Console.WriteLine("Package added!");
                                break;

                            case "2":
                                system.ProcessDeliveries();
                                undoStack.Push("ProcessDeliveries");
                                Console.WriteLine("Deliveries processed!");
                                break;

                            case "3":
                                system.SortPackages();
                                Console.WriteLine("Packages sorted!");
                                break;

                            case "4":
                                Console.Write("Enter package ID: ");
                                int searchId = int.Parse(Console.ReadLine());

                                Package found = system.SearchPackageById(searchId);

                                if (found != null)
                                    Console.WriteLine("Package found!");
                                else
                                    Console.WriteLine("Not found.");
                                break;

                            case "5":
                                system.SimulateDay();
                                Console.WriteLine("Simulation complete!");
                                break;

                            case "6":
                                if (!undoStack.IsEmpty())
                                {
                                    string lastAction = undoStack.pop();
                                    Console.WriteLine("Undo: " + lastAction);
                                }
                                else
                                {
                                    Console.WriteLine("Nothing to undo.");
                                }
                                break;

                            case "7":
                                fileManager.loadedPackages = system.GetAllPackages();
                                fileManager.Save("data.txt");
                                Console.WriteLine("Saved!");
                                break;

                            case "8":
                                fileManager.Load("data.txt");
                                Console.WriteLine("Loaded!");
                                break;

                            case "0":
                                running = false;
                                break;

                            default:
                                Console.WriteLine("Invalid option.");
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
