using SmartLogisticsDeliverySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics___Delivery_System
{
    internal class CustomStack<T> : IStackable<T>
    {
        private List<T> items = new List<T>();
        public bool IsEmpty()
        {
            return items.Count == 0;
        }
        public T Peek()
        {
            if (IsEmpty())
                throw new Exception("The stack is empty");
            return items[items.Count - 1];
        }
        public T pop()
        {
            if (IsEmpty())
                throw new Exception("The stack is empty");

            T last = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return last;
        }
        public void Push(T item)
        {
            items.Add(item);
        }
    }
}
