using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // Example 6-35
    public class BaseWithZeroArgCtor
    {
        public BaseWithZeroArgCtor()
        {
            Console.WriteLine("Base constructor");
        }
    }

    public class DerivedNoDefaultCtor : BaseWithZeroArgCtor
    {
        public DerivedNoDefaultCtor(int i)
        {
            Console.WriteLine("Derived constructor");
        }
    }

    // Example 6-36
    public class BaseNoDefaultCtor
    {
        public BaseNoDefaultCtor(int i)
        {
            Console.WriteLine("Base constructor: " + i);
        }
    }

    public class DerivedCallingBaseCtor : BaseNoDefaultCtor
    {
        public DerivedCallingBaseCtor()
            : base(123)
        {
            Console.WriteLine("Derived constructor (default)");
        }

        public DerivedCallingBaseCtor(int i)
            : base(i)
        {
            Console.WriteLine("Derived constructor: " + i);
        }
    }

    // Example 6-37
    public class BaseInit
    {
        protected static int Init(string message)
        {
            Console.WriteLine(message);
            return 1;
        }

        private int b1 = Init("Base field b1");

        public BaseInit()
        {
            Init("Base constructor");
        }

        private int b2 = Init("Base field b2");
    }

    public class DerivedInit : BaseInit
    {
        private int d1 = Init("Derived field d1");

        public DerivedInit()
        {
            Init("Derived constructor");
        }

        private int d2 = Init("Derived field d2");
    }

}
