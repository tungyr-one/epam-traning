using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectangleHelper
{
    public static class RectangleHelper
    {
        // rectangle perimeter calculation method
        public static void RectanglePerimeter(double height, double width)
        {
            if (height <= 0 || width <= 0)
            {
                Console.WriteLine("One of side is less or equal 0!");
            }
            else
            {
                double result = 2 * (height + width);
                Console.WriteLine("Rectangle perimeter: " + result);
            }
        }

        // rectangle square calculation method
        public static void RectangleSquare(double height, double width)
        {
            if (height <= 0 || width <= 0)
            {
                Console.WriteLine("One of side is less or equal 0!");
            }
            else
            {
                double result = height * width;
                Console.WriteLine("Rectangle square: " + result);
            }
        }
    }
}
