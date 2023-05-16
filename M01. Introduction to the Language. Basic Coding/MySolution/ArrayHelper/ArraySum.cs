using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayHelper
{
    public static class ArraySum
    {
        public static void SumOfArrayElements(int[,] arr)
        {
            int result = 0;
            //get through all elements of array and sum it up
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] >= 0)
                    {
                        result += arr[i, j];
                    }
                }

            }
            Console.WriteLine("Array posisive elements sum: " + result);
        }
    }
}
