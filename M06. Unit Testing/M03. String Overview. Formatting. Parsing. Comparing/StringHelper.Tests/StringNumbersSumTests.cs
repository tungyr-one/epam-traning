using NUnit.Framework;

namespace StringHelper.Tests
{
    public class StringNumbersSumTests
    {
        [TestCase("92233720368547758089", "92233720368547758099441354", "92233812602268126647199443")]
        [TestCase("1", "12346", "12347")]
        [TestCase("0", "12346", "12346")]
        public void StringNumbersSum_Test(string first, string second, string expectedResult)
        {
            // act 
            string actual = StringNumbersSum.CalcNumbersSum(first, second);

            // assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [TestCase("A", "123", null)]
        [TestCase("123", "A", null)]
        public void CalcNumbersSum_A_and_123returned_null(string first, string second, string expectedResult)
        {
            // act 
            string actual = StringNumbersSum.CalcNumbersSum(first, second);

            // assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

    }
}