using System;
using System.Collections.Generic;

namespace PolishNotationCalc
{
    internal class Program
    {
        public static void Main()
        {
            char[] splitSign = new char[] { ' ', '\t' };
            for (; ; )
            {
                string userInput = Console.ReadLine();
                if (userInput == null) break;
                Stack<string> tokens = new Stack<string>
                        (userInput.Split(splitSign, StringSplitOptions.RemoveEmptyEntries));
                if (tokens.Count == 0) continue;

                try
                {
                    double result = PolishNotationCalculator.EvaluateNotation(tokens);
                    if (tokens.Count != 0) throw new Exception();
                    Console.WriteLine(result);
                }
                catch (Exception e) { Console.WriteLine("Error during evaluation!"); }
                break;
            }
        }
    }    
}
