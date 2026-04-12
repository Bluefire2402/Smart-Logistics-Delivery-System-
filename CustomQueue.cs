using SmartLogisticsDeliverySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart_Logistics___Delivery_System
{
    internal class CustomQueue<T> : IQueueable<T>
    {
        private List<T> items = new List<T>();
        public T Dequeue()
        {
            if (IsEmpty())
                throw new Exception("Queue is empty");
            T first = items[0];
            items.RemoveAt(0);
            return first;
        }
        public void Enqueue(T item)
        {
            items.Add(item);
        }
        public bool IsEmpty()
        {
            return items.Count == 0;
        }
        public T Peek()
        {
            if (IsEmpty())
                throw new Exception("Queue is Empty");
            return items[0];
        }
    }
    
}
