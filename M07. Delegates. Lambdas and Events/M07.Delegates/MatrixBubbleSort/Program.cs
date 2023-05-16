using System;

namespace MatrixBubbleSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //int[,] userArr = new int[3, 2] {{5, 9 },
            //                            { 6, 7 },
            //                            { 1, 2 } };

            //int[,] userArr = new int[6, 3] {{6, 7, 4 },
            //                            { 3, 4, 9 },
            //                            { 1, 2, 4 },
            //                            { 0, 3, 3 },
            //                            { -7, 2, 1 },
            //                            { 8, 9, 0 } };

            int[,] userArr = new int[3, 2] { { 0, 1 }, { 1, -2 }, { 6, 7 } };
            //int[,] userArr = new int[3, 2] { { 0, 3 }, { 0, 2 }, { 0, 1 } };


            var matrixSort = new MatrixSort();

            //int [,] _arr = matrixSort.GetSortedArr(MatrixBubbleSort.BubbleSort, userArr, "sum", "ascending");
            int [,] _arr = matrixSort.GetSortedArr(MatrixBubbleSort.BubbleSort, userArr, "max", "ascending");
            Console.WriteLine("---");



            ////int[,] arr = new int[3, 2] {{6, 7 },
            ////                            { 3, 4 },
            ////                            { 1, 2 } };

            //int[,] userArr = new int[3, 2] {{5, 9 },
            //                            { 6, 7 },
            //                            { 1, 2 } };

            ////int[,] userArr = new int[6, 3] {{6, 7, 4 },
            ////                            { 3, 4, 9 },
            ////                            { 1, 2, 4 },
            ////                            { 0, 3, 3 },
            ////                            { -7, 2, 1 },
            ////                            { 8, 9, 0 } };

            //Console.WriteLine("--------------------------");


            //MatrixBubbleSort sumSort = new(userArr);
            ////int[,] _arr = sumSort.BubbleSort("sum", "ascending");
            ////int[,] _arr = sumSort.BubbleSort("sum", "descending");

            //MatrixBubbleSort maxSort = new(userArr);
            ////int[,] _arr = maxSort.BubbleSort("max", "ascending");
            ////int[,] _arr = maxSort.BubbleSort("max", "descending");

            //MatrixBubbleSort minSort = new(userArr);
            ////int[,] _arr = minSort.BubbleSort("min", "ascending");
            ////int[,] _arr = minSort.BubbleSort("min", "descending");


            Console.WriteLine("--------------------------");
            for (int i = 0; i < _arr.GetLength(0); i++)
            {
                for (int j = 0; j < _arr.GetLength(1); j++)
                {
                    Console.Write(_arr[i, j]);
                }
                Console.Write(" - ");
            }


            //Console.ReadKey();
        }
    }
}
