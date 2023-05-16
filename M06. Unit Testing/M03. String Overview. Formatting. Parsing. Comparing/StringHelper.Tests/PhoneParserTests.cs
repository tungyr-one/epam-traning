using NUnit.Framework;
using System;
using System.IO;

namespace StringHelper.Tests
{
    public class PhoneParserTests
    {
        private const string testFileName = "\\Test.txt";

        [SetUp]
        public void Setup()
        {
            try
            {
                string text = "Bla bla bla my number is +7 (921) 345-67-89 kekeke Blo Blo blo +375 (34) 444-7843 ololo bla bla bla 8 921 325-45-78";
                StreamWriter sw = new(AppContext.BaseDirectory + testFileName);
                sw.Write(text);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        [Test]
        public void FindPhone_Text_file()
        {
            // arrange
            string a = AppContext.BaseDirectory + testFileName;
            string expected = "+7 (921) 345-67-89\n+375 (34) 444-7843\n8 921 325-45-78\n";

            // act 
            string actual = PhoneParser.FindPhone(a);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}