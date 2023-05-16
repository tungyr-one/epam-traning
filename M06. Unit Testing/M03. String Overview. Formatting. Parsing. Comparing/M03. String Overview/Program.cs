using System;

namespace StringHelper
{
    internal class Program
    {

        internal static void Main(string[] args)
        {
            ////// 2. Doubling characters in the first string depending on the presence of identical characters in the second
            //Console.WriteLine(DoublingChars.AddDoubleChar("Hello everybody", "helloween"));

            ////// 3. Sum of big numbers using strings only
            //Console.WriteLine(StringNumbersSum.CalcNumbersSum("92233720368547758089", "92233720368547758099441354"));

            ////// 4. Reverse words in string
            //Console.WriteLine(ReversedString.ReverseWords("The greatest victory is that which requires no battle"));

            //// 5. Phone numbers finder
            PhoneParser.FindPhone(AppContext.BaseDirectory + "\\Text.txt");
        }
    }     
}
