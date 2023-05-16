using System;
using System.Linq;

namespace StringHelper
{
    internal static class StringNumbersSum
    {
        internal static string CalcNumbersSum(string firstNumber, string secondNumber)
        {
            foreach (char c in firstNumber)
            {
                if (c < '0' || c > '9')
                {
                    Console.WriteLine($"Invalid number \"{c}\" found in first number! ");
                    return null;
                }
            }

            foreach (char c in secondNumber)
            {
                if (c < '0' || c > '9')
                {
                    Console.WriteLine($"Invalid number \"{c}\" found in first number! ");
                    return null;
                }
            }

            var res = string.Empty;
            int remainder, additional = 0;

            var maxLen = Math.Max(firstNumber.Length, secondNumber.Length);
            if (firstNumber.Length < secondNumber.Length)
            {
                firstNumber = WidenString(firstNumber, maxLen);
            }
            else if (firstNumber.Length > secondNumber.Length)
            {
                secondNumber = WidenString(secondNumber, maxLen);
            }

            for (int i = maxLen - 1; i >= 0; i--)
            {
                remainder = (additional + (int)Char.GetNumericValue(firstNumber[i]) + (int)Char.GetNumericValue(secondNumber[i])) % 10;
                res += remainder.ToString();
                additional = (additional + (int)Char.GetNumericValue(firstNumber[i]) + (int)Char.GetNumericValue(secondNumber[i])) / 10;
            }

            return new String(res.Reverse().ToArray());
        }

        internal static string WidenString(string number, int requiredLength)
        {
            return number = new string('0', requiredLength - number.Length) + number;
        }

    }
}
