using System;

namespace LibsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" === LIBRARIES DEMONSTRATION === " + "\n");

            Console.WriteLine(" BUBBLE SORT: " + "\n");

            // user input array size
            Console.WriteLine("Insert size of array:");
            var arraySize = InputValidation();

            //user input array elements
            Console.WriteLine("Insert " + arraySize + " elements of array:");
            int[] userArray = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                userArray[i] = InputValidation();
            }

            ArrayHelper.ArrayHelper.SortedArrayPrint(userArray);
            ArrayHelper.ArrayHelper.BubbleSortAsc(userArray);
            ArrayHelper.ArrayHelper.BubbleSortDesc(userArray);



            Console.WriteLine("\n" + " TWO DIMENSIONAL ARRAY SUM: " + "\n");

            // user input array size
            Console.WriteLine("Insert two dimensional array size:");
            int[] doubleArraySize = new int[2];
            for (int i = 0; i < 2; i++)
            {
                doubleArraySize[i] = InputValidation();
            }

            int[,] calcArray = new int[doubleArraySize[0], doubleArraySize[1]];

            // user input array elements
            Console.WriteLine("Insert elements of array:");
            for (int i = 0; i < doubleArraySize[0]; i++)
            {
                for (int j = 0; j < doubleArraySize[1]; j++)
                {
                    calcArray[i, j] = InputValidation();
                }
            }

            ArrayHelper.ArraySum.SumOfArrayElements(calcArray);




            Console.WriteLine("\n" + " RECTANGLE PERIMETER AND SQUARE: " + "\n");

            // user input rectangle size
            Console.WriteLine("Insert length and width of rectangle:");
            double[] rectangleSize = new double[2];
            for (int i = 0; i < 2; i++)
            {
                string rectangleSide = Console.ReadLine();
                while (!double.TryParse(rectangleSide, out _))
                {
                    Console.WriteLine("Try again");
                    rectangleSide = Console.ReadLine();
                }
                rectangleSize[i] = Convert.ToDouble(rectangleSide);
            }

            RectangleHelper.RectangleHelper.RectanglePerimeter(rectangleSize[0], rectangleSize[1]);
            RectangleHelper.RectangleHelper.RectangleSquare(rectangleSize[0], rectangleSize[1]);

            Console.ReadLine();


            // user input validation method
            static int InputValidation()
            {
                string userInput = Console.ReadLine();
                while (!int.TryParse(userInput, out _))
                {
                    Console.WriteLine("Try again");
                    userInput = Console.ReadLine();
                }
                return Convert.ToInt32(userInput);
            }
        }
    }
}
