using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Example1
{
    public class SomeClass
    {
    }

    // Example 6-1
    public class Derived : SomeClass
    {
    }

    public class AlsoDerived : SomeClass, IDisposable
    {
        public void Dispose() { }
    }
}
