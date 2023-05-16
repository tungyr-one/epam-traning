namespace BusinessLogic.BusinessLogic.Notifier
{
    public interface INotifyManager
    {
        public void SetMessageSender(IMessageSender sender);

        public void Notify(string contact, string msg);
    }
}