using System;

namespace BusinessLogic.BusinessLogic.Notifier
{
    internal class EmailSender : IMessageSender
    {
        public void SendMessage(string contact, string msg)
        {
            Console.WriteLine($"Sent e-mail msg \"{msg}\" to {contact}");
        }
    }
}