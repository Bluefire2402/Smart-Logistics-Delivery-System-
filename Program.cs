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
