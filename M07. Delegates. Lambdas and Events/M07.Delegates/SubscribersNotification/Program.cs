using System;

namespace SubscribersNotification
{
    internal class Program
    {
        internal static void Main()
        {
            //Console.WriteLine("Enter message: ");
            //string userMessage = Console.ReadLine();

            //Console.WriteLine("Enter timeout in miliseconds: ");
            //string userTimeout = Console.ReadLine();


            //CountdownTransmitter trans = new(userMessage, userTimeout);

            CountdownTransmitter trans = new("", "");
            SubscriberOne subOne = new();
            SubscriberTwo subTwo = new();
            subOne.Subscribe(trans);
            subTwo.Subscribe(trans);

            trans.Run();
        }
    }
}

