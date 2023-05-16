using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // Example 6-6
    interface IBase1
    {
        void Base1Method();
    }

    interface IBase2
    {
        void Base2Method();
    }

    interface IBoth : IBase1, IBase2
    {
        void Method3();
    }

    // Example 6-7
    public class Impl : IBoth
    {
        public void Base1Method()
        {
        }

        public void Base2Method()
        {
        }

        public void Method3()
        {
        }
    }
}