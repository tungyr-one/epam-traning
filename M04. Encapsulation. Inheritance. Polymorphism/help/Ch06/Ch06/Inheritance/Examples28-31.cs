using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // Example 6-28
    public class LibraryBase
    {
        public virtual void Start() { }
    }

    // Example 6-30
    public class CustomerDerived : LibraryBase
    {
        public new void Start()
        {
            Console.WriteLine("Derived type's Start method");
        }
    }

    class Examples28_31
    {
        public static void UsingHiddenAndVirtual()
        {
            // Example 6-29
            var d = new CustomerDerived();
            LibraryBase b = d;

            d.Start();
            b.Start();

        }
    }

    // Commented out because this represents an excerpt from the .NET Framework class library
    // Example 6-31
    //public interface ISet<T> : ICollection<T>
    //{
    //    new bool Add(T item);
    //    ... other members omitted for clarity
    //}

}
