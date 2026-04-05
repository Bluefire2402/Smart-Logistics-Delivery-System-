using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    namespace SmartLogisticsDeliverySystem 
{
    
        internal class FileManager : IFileManager
        {
            public List<Package> loadedPackages = new List<Package>();
            public void Load(string path)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("File not found.");
                    return;
                }

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

                Console.WriteLine("Data loaded successfully.");
            }



            public void Save(string path)
        {
            throw new NotImplementedException();
        }
    }
}
