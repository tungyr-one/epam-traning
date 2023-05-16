using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class FibonacciNumbersGenerator
    {        
        public static IEnumerable<int> GenerateFibonacciNumbers()
        {
            var previousValueOne = 0;
            var previousValueTwo = 1;

            while (true)
            {
                int temp = previousValueOne;
                previousValueOne = previousValueTwo;
                previousValueTwo = temp + previousValueTwo;
                yield return previousValueOne;
            }
        }
    }
}
