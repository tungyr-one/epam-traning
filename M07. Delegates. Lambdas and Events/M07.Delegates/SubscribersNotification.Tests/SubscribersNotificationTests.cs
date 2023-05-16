using System;
using NUnit.Framework;

namespace SubscribersNotification.Tests
{
    public class SubscribersNotificationTests
    {
        [TestCase("", "", typeof(ArgumentException))]
        [TestCase("", "300", typeof(ArgumentException))]
        [TestCase("msg", "msg", typeof(ArgumentException))]
        public void ThrowArgumentException(string msg, string timeout, Type expectedException)
        {
            Assert.Throws(expectedException, () => new CountdownTransmitter(msg, timeout));
        }

        [Test]
        public void msg_and_100_got_msg()
        {
            // Act
            CountdownTransmitter testTrans = new("msg", "100");
            SubscriberOne subOne = new();
            subOne.Subscribe(testTrans);
            testTrans.Run();

            var result = subOne.ReceivedMsg;
            var expectedResult = "msg";

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}