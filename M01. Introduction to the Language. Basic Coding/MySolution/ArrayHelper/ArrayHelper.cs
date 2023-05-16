using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayHelper
{
    public static class ArrayHelper
    {
        // ascending array bubble sort method
        public static void BubbleSortAsc(int[] arr)
        {
            int len = arr.Length;
            for (int i = 0; i < len - 1; i++)
                for (int j = 0; j < len - i - 1; j++)
                    if (arr[j] > arr[j + 1])
                    {
                        // swap values with tmp variable
                        int tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                    }
            Console.Write("Sorted ascended array: ");
            SortedArrayPrint(arr);


        }
        // descending array bubble sort method
        public static void BubbleSortDesc(int[] arr)
        {
            int num = arr.Length;
            for (int i = 0; i < num - 1; i++)
                for (int j = 0; j < num - i - 1; j++)
                    if (arr[j] < arr[j + 1])
                    {
                        // swap values with tmp variable
                        int tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                    }

            Console.Write("Sorted descended array: ");
            SortedArrayPrint(arr);
        }

        // printing array
        public static void SortedArrayPrint(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
    }
}
