using System;

namespace SubscribersNotification
{
    internal class CountdownTransmitter
    {
        public int TimeOut { get; set; }
        public string Message { get; set; }

        internal CountdownTransmitter(string userMessage, string userTimeout)
        {
            ValidateParameters(new Tuple<string, string>(userMessage, userTimeout));

            Message = userMessage;
            this.TimeOut = Convert.ToInt32(userTimeout);
        }

        internal delegate void TransmitterHandler(CountdownTransmitter trans);
        internal TransmitterHandler Transmit;

        internal void Run()
        {
            System.Threading.Thread.Sleep(TimeOut);
            if (Transmit != null)
            {
                Transmit.Invoke(this);
            }
        }

        private void ValidateParameters(Tuple<string, string> parameters)
        {
            if (string.IsNullOrEmpty(parameters.Item1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ArgumentException("Wrong parameter!");
            }

            foreach (char c in parameters.Item2)
            {
                if (c < '0' || c > '9')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new ArgumentException("Wrong parameter!"); 
                }
            }
        }
    }
}
