namespace BusinessLogic.BusinessLogic.Notifier
{
    public interface IMessageSender
    {
        public void SendMessage(string contact, string msg);
    }
}