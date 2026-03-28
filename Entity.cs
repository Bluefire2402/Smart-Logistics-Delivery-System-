using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Entity
    {
        public int id;
        public string name;
        public DateTime createdDate;
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new HandleException.InvalidNameException();
            }
            this.name = name;

        }
        public string GetName()
        {
            return name;
        }
        public virtual bool Validate()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new HandleException.InvalidObjectDate();
            }
            return true;
        }
        public abstract void Display();
    }
}
