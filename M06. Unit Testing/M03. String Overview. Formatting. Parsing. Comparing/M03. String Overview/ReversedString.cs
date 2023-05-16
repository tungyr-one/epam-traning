using System;
using System.Text;

namespace StringHelper
{
    internal static class ReversedString
    {
        internal static string ReverseWords(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                Console.WriteLine("Invalid input string!");
                return null;
            }

            var result = new StringBuilder();
            string[] words = str.Split();

            var i = words.Length;
            while (i != 0)
            {
                i--;
                result.Append(words[i] + " ");
            }

            return result.ToString().Trim();
        }
    }
}
