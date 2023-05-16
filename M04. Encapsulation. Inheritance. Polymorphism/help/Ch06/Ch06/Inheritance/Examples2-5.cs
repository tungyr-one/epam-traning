using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // Example 6-2
    public class Base
    {
    }

    public class Derived : Base
    {
    }

    public class MoreDerived : Derived
    {
    }

    class Examples2_5
    {
        // Example 6-3
        public static void UseAsDerived(Base baseArg)
        {
            var d = (Derived) baseArg;

            //... go on to do something with d
        }

        // Example 6-4
        public static void MightUseAsDerived(Base b)
        {
            var d = b as Derived;

            if (d != null)
            {
                // ... go on to do something with d
            }
        }

        public static void UseIs()
        {
            object b = null;

            // Example 6-5
            if (!(b is WeirdType))
            {
                //    ... do the processing that everything except WeirdType requires
            }
        }
    }

    public class WeirdType
    {
    }
}
