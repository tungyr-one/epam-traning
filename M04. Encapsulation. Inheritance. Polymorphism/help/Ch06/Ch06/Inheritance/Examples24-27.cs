using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Examples24to27
{
    // Example 6-24
    public abstract class AbstractBase
    {
        public abstract void ShowMessage();
    }

    // Example 6-25
    public abstract class MustBeComparable : IComparable<string>
    {
        public abstract int CompareTo(string other);
    }

    // Example 6-26
    public class LibraryBase
    {
    }

    // Example 6-27
    public class CustomerDerived : LibraryBase
    {
        public void Start()
        {
            Console.WriteLine("Derived type's Start method");
        }
    }
}
