using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    public abstract class Entity
    {
        protected int id;
        protected string name;
        protected DateTime createdDate;
        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new HandleException.InvalidNameException();
            }
            this.name = name;

        }
        public Entity()
        {
            createdDate = DateTime.Now;
        }
        public string GetName()
        {
            return name;
        }
        public int GetId()
        {
            return id;
        }
        public void SetId(int id)
        {
            if (id <= 0)
            {
                throw new HandleException.InvalidIdException();
            }
            this.id = id;
        }
        public DateTime GetCreatedDate()
        {
            return createdDate;
        }
        public virtual bool Validate()
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new HandleException.InvalidObjectData();
            }
            return true;
        }
        public abstract void Display();
    }
}
