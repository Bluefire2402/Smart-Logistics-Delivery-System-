using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class HandleException
    {
        public class InvalidNameException : Exception
        {
            public InvalidNameException()
                : base("Name cannot be empty")
            {
            }

            public InvalidNameException(string message)
                : base(message)
            {
            }
        }
        public class InvalidObjectDate : Exception
        {
            public InvalidObjectDate()
                : base("Object can not be null or empty")
            {
            }

            public InvalidObjectDate(string message)
                : base(message)
            {
            }
        }
        public class InvalidCapacity : Exception
        {
            public InvalidCapacity()
                : base("Object can not be null or empty")
            {
            }

            public InvalidCapacity(string message)
                : base(message)
            {
            }
        }
        public class InvalidPackageException : Exception
        {
            public InvalidPackageException()
                : base("Package is invalid")
            {
            }
            public InvalidPackageException(string message)
                : base(message)
            {
            }
        }
        public class NoBestWorkerException : Exception
        {
            public NoBestWorkerException()
                : base("No best worker found")
            {
            }
            public NoBestWorkerException(string message)
                : base(message)
            {
            }

        }
        public class InvalidStatus : Exception
        {
            public InvalidStatus()
                : base("Status is invalid")
            {
            }
            public InvalidStatus(string message)
                : base(message)
            {
            }
        }
        public class InvalidVehicleException : Exception
        {
            public InvalidVehicleException()
                : base("Vehicle is invalid")
            {
            }
            public InvalidVehicleException(string message)
                : base(message)
            {
            }
        }
        public class NoBestVehicleException : Exception
        {
            public NoBestVehicleException()
                : base("No best vehicle found")
            {
            }
            public NoBestVehicleException(string message)
                : base(message)
            {
            }
        }
        public class NoPendingPackagesException : Exception
        {
            public NoPendingPackagesException()
                : base("No pending packages found")
            {
            }
            public NoPendingPackagesException(string message)
                : base(message)
            {
            }
        }
    }