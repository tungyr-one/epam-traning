using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Examples32to34
{
    // Example 6-32
    public class FixedToString
    {
        public sealed override string ToString()
        {
            return "Arf arf!";
        }
    }

    // Example 6-33
    public sealed class EndOfTheLine
    {
    }

    // Example 6-34
    public class CustomerDerived : LibraryBase
    {
        public override void Start()
        {
            Console.WriteLine("Derived type's Start method");
            base.Start();
        }
    }
}
