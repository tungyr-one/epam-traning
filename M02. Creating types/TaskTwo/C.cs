using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
    internal class C: IComparable
    {
        internal int i;

        public int CompareTo(object obj)
        {
            if (obj is C otherClass)
            {
                return this.i.CompareTo(otherClass.i);
            }
            else
            {
                throw new ArgumentException("Object is not С class");
            }
        }
    }
}
