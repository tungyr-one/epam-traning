using System;
using System.Diagnostics;

namespace Performance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var p = Process.GetCurrentProcess();           
            p.Refresh();
            var classMemBefore = p.PrivateMemorySize64;

            Random randInt = new();
            C[] classes = new C[100000];
            for (int elem = 0; elem < classes.Length; elem ++)
            {
                classes[elem] = new C
                {
                    i = randInt.Next()
                };
            }

            p.Refresh();   
            var classMemAfter = p.PrivateMemorySize64;

            p.Refresh();            
            var structMemBefore = p.PrivateMemorySize64;

            S[] structs = new S[100000];
            for (int elem = 0; elem < structs.Length; elem ++)
            {
                structs[elem] = new S
                {
                    i = randInt.Next()
                };
            }

            p.Refresh();
            var structMemAfter = p.PrivateMemorySize64;

            var classesDelta = classMemAfter - classMemBefore;
            var structsDelta = structMemAfter - structMemBefore;

            Console.WriteLine("Classes array initialization delta:" + (classesDelta));
            Console.WriteLine("Structs array initialization delta:" + (structsDelta));
            Console.WriteLine("Difference between two deltas :" + ((classesDelta - structsDelta)));



            var classWatch = new Stopwatch();
            classWatch.Start();
            Array.Sort<C>(classes);
            classWatch.Stop();

            Console.WriteLine($"Class array sort execution time: {classWatch.ElapsedMilliseconds} ms");

            var structWatch = new Stopwatch();
            structWatch.Start();
            Array.Sort<S>(structs);
            structWatch.Stop();

            Console.WriteLine($"Struct array sort execution time: {structWatch.ElapsedMilliseconds} ms");
        }
    }
}
