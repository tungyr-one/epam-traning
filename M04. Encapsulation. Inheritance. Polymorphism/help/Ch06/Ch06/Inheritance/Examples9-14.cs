using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Examples9_14
    {
        // Example 6-9
        public static void UseBase(Base b)
        {
        }

        // Example 6-10
        public static void AllYourBase(IEnumerable<Base> bases)
        {
        }

        public static void PassDerivedItemsAsEnumerable()
        {
            // Example 6-11
            IEnumerable<Derived> derivedBases =
                new Derived[] { new Derived(), new Derived() };
            AllYourBase(derivedBases);
        }

        // Example 6-12
        public static void AddBase(ICollection<Base> bases)
        {
            bases.Add(new Base());
        }

        //// Commented out because this illustrates an error
        //public static void BadAttemptToPassDerivedItemsAsCollection()
        //{
        //    // Example 6-13
        //    ICollection<Derived> derivedList = new List<Derived>();
        //    AddBase(derivedList);  // Will not compile
        //}
    }

    // This is an excerpt from a type definition in the .NET Framework Class Library,
    // so it's commented out here - it's not meant to be compiled in isolation.
    // Example 6-14
    // public interface IEnumerable<out T> : IEnumerable
}
