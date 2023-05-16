using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
    internal class S: IComparable
    {
        internal int i;

        public int CompareTo(object obj)
        {
            S otherStruct = (S)obj;
            return this.i.CompareTo(otherStruct.i);
        }
    }
}
