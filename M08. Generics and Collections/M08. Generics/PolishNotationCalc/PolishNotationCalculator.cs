using System;
using System.Collections.Generic;

namespace PolishNotationCalc
{
    internal static class PolishNotationCalculator
    {
        internal static double EvaluateNotation(Stack<string> tokens)
        {
            string token = tokens.Pop();
            double x, y;
            if (!Double.TryParse(token, out x))
            {
                y = EvaluateNotation(tokens);
                x = EvaluateNotation(tokens);
                switch (token)
                {
                    case "+":
                        x += y;
                        break;
                    case "-":
                        x -= y;
                        break;
                    case "*":
                        x *= y;
                        break;
                    case "/":
                        x /= y;
                        break;
                }
            }
            return x;
        }
    }
}
