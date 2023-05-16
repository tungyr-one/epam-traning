using NUnit.Framework;
using Microsoft.Extensions.Logging.Abstractions;

namespace Converter.Tests
{
    public class ConverterTests
    {
        [TestCase("86483", 86483)]
        [TestCase("6.5", 0)]
        [TestCase("0", 0)]
        [TestCase(" ", 0)]        
        [TestCase("!example", 0)]        
        public void StrConverter_ConvertStrToInt_Test(string number, int expectedResult)
        {
            // act 
            StrConverter conv = new(new NullLogger<StrConverter>());
            int actual = conv.ConvertStrToInt(number);

            // assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }
    }
}