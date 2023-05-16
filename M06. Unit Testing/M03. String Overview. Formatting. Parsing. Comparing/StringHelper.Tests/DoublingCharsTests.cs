using NUnit.Framework;

namespace StringHelper.Tests
{
    public class DoublingCharsTests
    {
        [TestCase("e", "e", "ee")]
        [TestCase("Hello 4 guys!:)", "helloween 4!:)", "Heelllloo 44 guys!!::))")]
        [TestCase("80# _!", "80# _!", "8800## __!!")]
        public void String_AddDoubleChars_Test(string first, string second, string expectedResult)
        {

            // act 
            string actual = DoublingChars.AddDoubleChar(first, second);

            // assert
            Assert.That(actual, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddDoubleChars_Hello_guys_and_empty_string_returned_null()
        {
            // arrange
            string a = "Hello 4 guys!:)";
            string b = "";
            string expected = null;

            // act 
            string actual = DoublingChars.AddDoubleChar(a, b);

            // assert
            Assert.AreEqual(expected, actual);
        }

    }
}