using System;

namespace BusinessLogic.Exceptions
{
    public class NotifyFail : Exception
    {
        public NotifyFail()
        {
        }

        public NotifyFail(string message)
            : base(message)
        { }
    }
}