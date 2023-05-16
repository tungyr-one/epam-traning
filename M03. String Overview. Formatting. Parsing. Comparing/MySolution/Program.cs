using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace StringHelper
{
    class Program
    {

        static void Main(string[] args)
        {
            //// 1. Average word length in an input string
            //string inputString = Console.ReadLine();
            ////string inputString = "Create a class that implements a method which allows to define an average word length in an input string";
            //Console.WriteLine(TaskOne.AverageWordLen(inputString));

            //// 2. Doubling characters in the first string depending on the presence of identical characters in the second
            //Console.WriteLine(TaskTwo.DoubleChar("Hello everybody", "helloween"));

            //// 3. Sum of big numbers using strings only
            //Console.WriteLine(TaskThree.numbersSum("92233720368547758089", "92233720368547758099441354"));
            ////Console.WriteLine((long)Convert.ToDouble(bigNumber1));

            //// 4. Reverse words in string
            //Console.WriteLine(TaskFour.WordsReverse("The greatest victory is that which requires no battle"));

            // 5. Phone numbers finder
            TaskFive.PhoneFinder(AppContext.BaseDirectory + "\\Text.txt");
        }
    }


    static class TaskOne
    {
        public static double AverageWordLen(string inputStr)
        {           
            // split string to separate words and sum every word length
            double result = 0;
            string[] words = inputStr.Split();
            for (int i = 0; i < words.Length; i++)
            {
                result += words[i].Length;
            }

            // define average words length
            result /= words.Length;
            return result;
        }
    }

    static class TaskTwo
    {
       public static StringBuilder DoubleChar(string firstStr, string secondStr)
        {
            // convert string to bundle of chars
            StringBuilder result = new();
            char[] firstChars = firstStr.ToCharArray();
            // check presence of first string chars in second string and add it to result
            for (int i = 0; i < firstChars.Length; i++)
            {
                if (secondStr.Contains(firstChars[i]) && firstChars[i].ToString() != " ")
                {
                    result.Append(new string(firstChars[i], 2));
                }
                else
                {
                    result.Append(firstChars[i]);
                }
            }
            return result;
       }
    }

    static class TaskThree
    {
        public static string numbersSum(string firstNumber, string secondNumber)
        {
            string res = "";
            int remainder, additional = 0;

            // make strings length even
            int maxLen = Math.Max(firstNumber.Length, secondNumber.Length);
            if (firstNumber.Length < secondNumber.Length)
            {
                firstNumber = new string('0', maxLen - firstNumber.Length) + firstNumber;
            }

            else if (firstNumber.Length > secondNumber.Length)
            {
                secondNumber = new string('0', maxLen - secondNumber.Length) + secondNumber;
            }

            // sum 
            for (int i = maxLen - 1; i >= 0; i--)
            {
                remainder = (additional + (int)Char.GetNumericValue(firstNumber[i]) + (int)Char.GetNumericValue(secondNumber[i])) % 10;
                res += remainder.ToString();
                additional = (additional + (int)Char.GetNumericValue(firstNumber[i]) + (int)Char.GetNumericValue(secondNumber[i])) / 10;
            }

            return new String(res.Reverse().ToArray());
        }
    }

    static class TaskFour
    {
        public static string WordsReverse(string str)
        {
            // split string to separate words
            var result = new StringBuilder();
            string[] words = str.Split();

            // add words to new string in backward order
            int i = words.Length;
            while (i != 0)
            {
                i--;
                result.Append(words[i] + " ");                
            }           

            // remove redundant spaces
            return result.ToString().Trim();
        }
    }

    static class TaskFive
    {
        static string text;
        public static void PhoneFinder(string path)
        {
            // open and read file
            OpenFile(path);
            Console.WriteLine("Text read from file: \n" + text);
            ////Regex formatOne = new Regex(@"/^(?:\(\d{3}\)|\d{3}-)\d{3}-\d{4}$/");
            //Regex formatOne = new Regex(@"\+\d\s\(\d{3}\)\s\d{3}\-\d{2}-\d{2}");
            //Regex formatTwo = new Regex(@"\+\d{3}\s\(\d{2}\)\s\d{3}\-\d{4}");
            //Regex formatThree = new Regex(@"\d\s\d{3}\s\d{3}\-\d{2}\-\d{2}");
            //// together for +7 (921) 345-67-89 and +375 (34) 444-7843:
            //Regex formatTemp = new Regex(@"\+\d{1,3}\s\(\d{2,3}\)\s\d{3}\-\d{2,4}\-?\d{2}?");

            // searching phone numbers with regex pattern and collect numbers to collection
            Regex pattern = new(@"\+?\d{1,3}\s\(?\d{2,3}\)?\s\d{3}\-\d{2,4}\-?\d{2}?");
            MatchCollection matches = pattern.Matches(text);

            // convert found matches to strings
            var phones = new StringBuilder();
            for (int i = 0; i < matches.Count; i++)
            {
                phones.Append(matches[i].ToString() + "\n");
            }

            // write found found numbers to file
            WriteFile(phones);
        }

        public static string OpenFile(string path)
        //public static string OpenFile()
        {
            // opening file and read text
            try
            {
                StreamReader sr = new StreamReader(path);
                text = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return text;
        }

        // write found phone numbers to file
        public static void WriteFile(StringBuilder numbers)
        {
            try
            {
                StreamWriter sw = new StreamWriter(AppContext.BaseDirectory + "\\Numbers.txt");
                sw.Write(numbers);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            Console.WriteLine("Found numbers are written to file: \n" + numbers);
        }
    }

}
