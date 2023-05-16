using BusinessLogic.BusinessLogic.Notifier;

namespace BusinessLogic
{
    internal class NotifyManager : INotifyManager
    {
        private IMessageSender _messageSender = null!;

        public NotifyManager()
        {
        }

        public void SetMessageSender(IMessageSender sender)
        {
            _messageSender = sender;
        }

        public NotifyManager(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public void Notify(string contact, string msg)
        {
            _messageSender.SendMessage(contact, msg);
        }
    }
}