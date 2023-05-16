using System;

namespace MatrixBubbleSort
{              
    internal class MatrixSort
    {
        public int[,] GetSortedArr(Func<int[,], string, string, int[,]> arrSort, int[,] inputArray, string comparisonType, string orderType)
        {
            return arrSort.Invoke(inputArray, comparisonType, orderType);
        }
    }
}
