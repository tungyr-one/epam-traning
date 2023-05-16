using System;
using System.Text;

namespace StringHelper
{
    internal static class DoublingChars
    {
        internal static string AddDoubleChar(string firstStr, string secondStr)
        {
            if(String.IsNullOrEmpty(firstStr) || String.IsNullOrEmpty(secondStr))
            { 
                Console.WriteLine("Invalid string parameters!");
                return null;
            }

            StringBuilder result = new();
            char[] firstStrArr = firstStr.ToCharArray();
            for (int i = 0; i < firstStrArr.Length; i++)
            {
                if (firstStrArr[i] == ' ')
                {
                    result.Append(firstStrArr[i]);
                }
                else
                {
                    if (secondStr.Contains(firstStrArr[i]))
                    {
                        result.Append(new string(firstStrArr[i], 2));
                    }
                    else
                    {
                        result.Append(firstStrArr[i]);
                    }
                }
            }
            return result.ToString();
        }
    }
}
