using System;

namespace SubscribersNotification
{
    internal class SubscriberTwo
    {
        public string ReceivedMsg { get; set; }

        internal void Subscribe(CountdownTransmitter transmitter)
        {
            transmitter.Transmit += GotMessage;
        }

        internal void GotMessage(CountdownTransmitter transmitter)
        {
            Console.WriteLine($"Subscriber Two got message: {transmitter.Message}");
            ReceivedMsg = transmitter.Message;
        }
    }
}
