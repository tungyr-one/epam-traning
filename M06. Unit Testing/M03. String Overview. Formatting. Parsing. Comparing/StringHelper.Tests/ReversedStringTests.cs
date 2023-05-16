using NUnit.Framework;
namespace StringHelper.Tests
{
    class ReversedStringTests
    {        
        [TestCase("The greatest victory is that which requires no battle",
            "battle no requires which that is victory greatest The")]
        [TestCase("", null)]
        public void ReversedString_Test(string str, string expectedResult)
        {
            // act 
            string actual = ReversedString.ReverseWords(str);

            // assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }       
    }
}
