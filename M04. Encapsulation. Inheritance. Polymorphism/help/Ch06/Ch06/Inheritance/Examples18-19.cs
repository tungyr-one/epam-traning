using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class Examples18_19
    {
        // Example 6-18
        public static void UseBaseArray(Base[] bases)
        {
            bases[0] = new Base();
        }

        public static void ChangeElementInArray()
        {
            var array = new Base[10];
            UseBaseArray(array);

            try
            {
                // Compiles, but throws.

                // Example 6-19
                Derived[] derivedBases = { new Derived(), new Derived() };
                UseBaseArray(derivedBases);
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
    }
}
