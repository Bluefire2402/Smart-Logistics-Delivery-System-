using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogisticsDeliverySystem
{
    public interface IFileManager
    {
        void Save(string path);
        void Load(string path);
    }
    public interface ISortable
    {
        void Sort();
    }
    public interface IQueueable<T>
    {
        void Enqueue(T item);
        T Dequeue();
        T Peek();
        bool IsEmpty();
    }
    public interface IStackable<T>
    {
        void Push(T item);
        T pop();
        T Peek();
        bool IsEmpty();
    }
}
