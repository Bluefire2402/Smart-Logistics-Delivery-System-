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
        public void Load(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("The file was not found");
                return;
            }
            loadedPackages.Clear();
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
                }
            }

            Console.WriteLine("The data was loaded.");
        }

        public void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (Package p in loadedPackages)
                {
                    writer.WriteLine($"PACKAGE|{p.GetId()}|{p.GetWeight()}|{p.GetPriorityLevel()}|{p.GetDestination()}|{p.GetStatus()}");
                }
            }
            Console.WriteLine("The data was saved ");
        }
        public void SetPackages(List<Package> packages)
        {
            loadedPackages = packages;
        }
        public List<Package> GetPackages()
        {
            return loadedPackages;
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
 
