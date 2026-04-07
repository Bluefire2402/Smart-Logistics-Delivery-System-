using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    internal interface IFileManager
    {
        void Save(string path);
        void Load(string path);
    }
    internal interface ISortable
    {
        void Sort();
    }
    internal interface IQueueable<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        bool IsEmpty();
    }
    internal interface IStackable<T>
    {
        void Push(T item);
        T pop();
        T Peek();
        bool IsEmpty();
    }

        
}
