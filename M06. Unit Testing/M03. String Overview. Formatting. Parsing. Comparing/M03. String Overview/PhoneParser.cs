using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace StringHelper
{
    internal static class PhoneParser
    {
        private static string _text;
        private const string ouputFileName = "\\Numbers.txt";

        internal static string FindPhone(string path)
        {
            OpenFile(path);
            Console.WriteLine("Text read from file: \n" + _text);

            Regex pattern = new(@"\+?\d{1,3}\s\(?\d{2,3}\)?\s\d{3}\-\d{2,4}\-?\d{2}?");
            MatchCollection matches = pattern.Matches(_text);

            var phones = new StringBuilder();
            for (int i = 0; i < matches.Count; i++)
            {
                phones.Append(matches[i].ToString() + "\n");
            }

            WriteFile(phones);
            return phones.ToString();
        }

        private static string OpenFile(string path)
        {
            try
            {
                StreamReader sr = new(path);
                _text = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            return _text;
        }

        private static void WriteFile(StringBuilder numbers)
        {
            try
            {
                StreamWriter sw = new(AppContext.BaseDirectory + ouputFileName);
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
