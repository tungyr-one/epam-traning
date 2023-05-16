using System;

namespace TaskOne
{
    class Program
    {
        static void Main(string[] args)
        {
            //    Qvadrate q = new(4);
            //    Rectangle r = new(2, 4);
            //    Circle c = new(6);
            //    Triangle t = new(2, 3, 4, 6);

            //    Console.WriteLine(q.Square());
            //    Console.WriteLine(q.Perimeter());

            //    Console.WriteLine(r.Square());
            //    Console.WriteLine(r.Perimeter());

            //    Console.WriteLine(c.Square());
            //    Console.WriteLine(c.Perimeter());

            //    Console.WriteLine(t.Square());
            //    Console.WriteLine(t.Perimeter());
        }

        // base class
        class Qvadrate // base class
        {
            public double side;

            public double Side
            {
                get { return side; }
                set { side = value; }
            }

            public Qvadrate(double side)
            {
                this.Side = side;
            }

            public virtual double Square()
            {
                return this.Side * this.Side;
            }

            public virtual double Perimeter()
            {
                return (this.Side + this.Side) * 2;
            }
        }


        class Rectangle : Qvadrate
        {
            public double Width { get; set; }

            public double Length { get; set; }

            public Rectangle(double width, double length) : base(width)
            {
                this.Width = width;
                this.Length = length;
            }

            public override double Square()
            {
                return this.Length * this.Width;
            }

            public override double Perimeter()
            {
                return (this.Length + this.Width) * 2;
            }
        }

        class Circle : Qvadrate
        {
            public double Radius { get; set; }

            public Circle(double radius) : base(radius)
            {
                this.Radius = radius;
            }

            public override double Square()
            {
                return Math.Pow((this.Radius * Math.PI), 2);
            }

            public override double Perimeter()
            {
                return 2 * Math.PI * this.Radius;
            }
        }

        class Triangle : Qvadrate
        {
            public double Side_a { get; set; }
            public double Side_b { get; set; }
            public double Side_c { get; set; }
            public double Height { get; set; }

            public Triangle(double side_a, double side_b, double side_c, double height) : base(height)
            {
                this.Side_a = side_a;
                this.Side_b = side_b;
                this.Side_c = side_c;
                this.Height = height;
            }

            public override double Square()
            {
                return (this.Side_a * this.Height) / 2;
            }

            public override double Perimeter()
            {
                return this.Side_a + this.Side_b + this.Side_c;
            }
        }
    }
}
