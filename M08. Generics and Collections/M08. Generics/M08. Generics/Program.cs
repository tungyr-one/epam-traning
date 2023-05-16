using ClassLibrary;
using ClassLibrary.Queue;
using ClassLibrary.Stack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace M08._Generics
{
    internal class Program    
    {
        enum CollectionType
        {
            Int,
            String
        }

        enum OperationType
        {
            Enqueue,
            Dequeue,
            Exit
        }

        internal static void Main(string[] args)
        {
            Console.WriteLine(" === FUNCTIONS DEMONSTRATION === " + "\n");

            Console.WriteLine(" BINARY SERCH: " + "\n");

            var wantedItem = 5;
            List<int> intList = new() { 5, 1, 0, 1, 9, -4 };

            Console.WriteLine($"Sorted list:");
            intList.Sort();
            intList.ForEach(s => Console.Write(s + " "));
            Console.WriteLine();
            Console.WriteLine("Search index of item: {0}", wantedItem);

            var searchResult = ClassLibrary.BinarySearcher.BinarySearch<int>(intList, wantedItem, Comparer<int>.Default);
            PrintBinarySearchResult(searchResult);
            Console.WriteLine();


            Console.WriteLine(" FIBONACCI NUMBERS: " + "\n");

            var numbersCount = 5;
            Console.Write("Qtty of Fibonacci numbers: {0}", numbersCount);
            Console.WriteLine("\n");
            var values = ClassLibrary.FibonacciNumbersGenerator.GenerateFibonacciNumbers().Take(numbersCount);

            foreach (int fib in values)
            {
                Console.Write(fib + " ");
            }
            Console.WriteLine("\n");


            Console.WriteLine("QUEUE AND STACK IMPLEMENTATION: " + "\n");

            var fibNumbers = ClassLibrary.FibonacciNumbersGenerator.GenerateFibonacciNumbers().Take(numbersCount);

            CustomQueue<int> intQueue = new();

            foreach (int fib in fibNumbers)
            {
                Console.WriteLine("Enqueue: {0}", fib);
                intQueue.Enqueue(fib);
            }

            Console.WriteLine("Enqueue: {0}", 10);
            intQueue.Enqueue(10);

            Console.WriteLine("Peek: {0}", intQueue.Peek());

            Console.WriteLine("Dequeue: {0}", intQueue.Dequeue());

            foreach (var node in intQueue)
            {
                Console.Write(node + " ");
            }

            //Console.ReadKey();

            //Console.WriteLine("Dequeue: {0}", intQueue.Dequeue());
            //Console.WriteLine("Peek: {0}", intQueue.Peek());
            //Console.WriteLine("Enqueue: {0}", 0);
            //intQueue.Enqueue(0);



            Console.WriteLine();
            Console.WriteLine("Stack:");

            CustomStack<int> intStack = new CustomStack<int>();

            //foreach (int fib in fibNumbers)
            //{
            //    Console.WriteLine("Push: {0}", fib);
            //    intStack.Push(fib);
            //}

            Console.WriteLine("Push: {0}", 9);
            intStack.Push(9);
            Console.WriteLine("Pop: {0}", intStack.Pop());
            Console.WriteLine("Pop: {0}", intStack.Pop());
            Console.WriteLine("Peek: {0}", intStack.Peek());
            //Console.WriteLine("Pop: {0}", intStack.Pop());

            //Console.WriteLine("Peek: {0}", intStack.Peek());
            //Console.WriteLine("Push: {0}", 999);
            //intStack.Push(999);

            foreach (var node in intStack)
            {
                Console.Write(node + " ");
            }
        }

        private static void PrintBinarySearchResult(int result)
        {
            if (result == -1)
                Console.WriteLine("Item not present");
            else
                Console.WriteLine("Item found at index " + result);
        }
    }
}