using System;

namespace BusinessLogic.BusinessLogic.Notifier
{
    internal class SmsSender : IMessageSender
    {
        public void SendMessage(string phone, string msg)
        {
            Console.WriteLine($"Sent SMS msg \"{msg}\" to {phone}");
        }
    }
}