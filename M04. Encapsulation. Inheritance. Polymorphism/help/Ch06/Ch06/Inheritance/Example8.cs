using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    public class GenericBase1<T>
    {
        public T Item { get; set; }
    }

    public class GenericBase2<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }

    public class NonGenericDerived : GenericBase1<string>
    {
    }

    public class GenericDerived<T> : GenericBase1<T>
    {
    }

    public class MixedDerived<T> : GenericBase2<string, T>
    {
    }
}
