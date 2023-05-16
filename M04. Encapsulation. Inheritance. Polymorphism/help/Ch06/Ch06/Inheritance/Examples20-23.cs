using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    // Example 6-20
    public class BaseWithVirtual
    {
        public virtual void ShowMessage()
        {
            Console.WriteLine("Hello from BaseWithVirtual");
        }
    }

    // Example 6-22
    public class DeriveWithoutOverride : BaseWithVirtual
    {
    }

    public class DeriveAndOverride : BaseWithVirtual
    {
        public override void ShowMessage()
        {
            Console.WriteLine("This is an override");
        }
    }


    class Examples20_23
    {
        // Example 6-21
        public static void CallVirtualMethod(BaseWithVirtual o)
        {
            o.ShowMessage();
        }

        public static void PassBase()
        {
            CallVirtualMethod(new BaseWithVirtual());
        }

        public static void UsingVirtualMethods()
        {
            // Example 6-32
            CallVirtualMethod(new BaseWithVirtual());
            CallVirtualMethod(new DeriveWithoutOverride());
            CallVirtualMethod(new DeriveAndOverride());
        }
    }
}
