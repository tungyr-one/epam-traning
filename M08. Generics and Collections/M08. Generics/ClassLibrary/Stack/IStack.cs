using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public interface IStack<T>
    {
        int Count { get; }
        T Peek();
        T Pop();
        void Push(T obj);
    }
}
