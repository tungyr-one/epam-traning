using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface IQueue<T>
    {
        int Count { get; }
        T Dequeue();
        void Enqueue(T obj);
        T Peek();
    }
}
