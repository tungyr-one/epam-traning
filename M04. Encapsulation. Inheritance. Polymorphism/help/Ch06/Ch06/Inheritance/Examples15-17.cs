using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inheritance
{
    // Example 6-15
    public class Shape
    {
        public Rect BoundingBox { get; set; }
    }

    public class RoundedRectangle : Shape
    {
        public double CornerRadius { get; set; }
    }

    // Example 6-16
    public class BoxAreaComparer : IComparer<Shape>
    {
        public int Compare(Shape x, Shape y)
        {
            double xArea = x.BoundingBox.Width * x.BoundingBox.Height;
            double yArea = y.BoundingBox.Width * y.BoundingBox.Height;

            return Math.Sign(xArea - yArea);
        }
    }

    public class CornerSharpnessComparer : IComparer<RoundedRectangle>
    {
        public int Compare(RoundedRectangle x, RoundedRectangle y)
        {
            // Smaller corners are sharper, so smaller radius is 'greater' for
            // the purpose of this comparison, hence the backwards subtraction.
            return Math.Sign(y.CornerRadius - x.CornerRadius);
        }
    }

    // This is an excerpt from a type definition in the .NET Framework Class Library,
    // so it's commented out here - it's not meant to be compiled in isolation.
    // Example 6-17
    // public interface IComparer<in T>
}
