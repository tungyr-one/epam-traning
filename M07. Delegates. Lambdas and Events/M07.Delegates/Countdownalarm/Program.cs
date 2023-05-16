using System;
using System.Threading;

namespace Countdownalarm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Message: "); 
            string message = Console.ReadLine();
            // NB: You would of course create a more sophisticated interface to 
            // let the user set the correct amount of time. For now, we'll just 
            // ask for how many seconds to wait
            Console.Write("How many seconds?: ");
            int seconds = Convert.ToInt32(Console.ReadLine());
            CountDownClock cdc = new CountDownClock(message, 0, 0, seconds); 
            CountDownTimerDisplay display = new CountDownTimerDisplay(cdc);
            CountDownTimerLog logger = new CountDownTimerLog(cdc); 
            cdc.Run();
        }
    }

    // a class to hold the message to display
    public class CountDownClockEventArgs : EventArgs
    {
        public readonly string message;
        public CountDownClockEventArgs(string message)
        {
            this.message = message;
        }
    }

    // The subject (publisher). The class to which other
    // classes will subscribe. Provides the delegate TimeExpired 
    // that fires when the requested amount of time has passed
    public class CountDownClock
    {
        private DateTime startingTime;
        private DateTime targetTime;
        private string message;

        // tell me the message to display, and how much time
        //(hours, minutes seconds) to wait
        public CountDownClock(string message, int hours, int mins, int seconds)
        {
            this.message = message;
            startingTime = DateTime.Now;
            TimeSpan duration = new TimeSpan(hours, mins, seconds); 
            targetTime = startingTime + duration;
        }

        // the delegate
        public delegate void TimesUpEventHandler(object countDownClock, CountDownClockEventArgs alarminformation);

        // an instance of the delegate
        public TimesUpEventHandler TimeExpired;

        // Check 10 times a second to see if the time has elapsed
        // if so, if anyone is listening, send then message
        public void Run()
        {
            for ( ; ; )
            {
                // sleep 1/10 of a second
                Thread.Sleep(100); // milliseconds

                // get the current time
                System.DateTime rightNow = System.DateTime.Now;
                if (rightNow >= this.targetTime)
                {
                    if (TimeExpired != null)
                    {
                        // Create the CountOownClockEventArgs to hold the message
                        CountDownClockEventArgs e = new CountDownClockEventArgs(this.message);
                        // fire the event
                        TimeExpired(this, e);
                        // stop the timer
                        break;
                    }   // end if registered delegates
                }   //	end	if time has passed
            }   //	end	forever loop
        }   //	end	run
    }   //	end	class

    // an observer.
    public class CountDownTimerDisplay
    {
        CountDownClock.TimesUpEventHandler myHandler;
        public CountDownTimerDisplay(CountDownClock cdc)
        {
            myHandler = new CountDownClock.TimesUpEventHandler(TimeExpired);
            // register the event handler and start the timer
            cdc.TimeExpired += myHandler;
        }
        // Alert the user that the time has expired
        public void TimeExpired(object theClock, CountDownClockEventArgs e)
        {
            Console.WriteLine(e.message);
        }
    }

    // an observer.
    public class CountDownTimerLog
    {
        CountDownClock.TimesUpEventHandler myHandler;
        public CountDownTimerLog(CountDownClock cdc)
        {
            myHandler = new CountDownClock.TimesUpEventHandler(TimeExpired);
            // register the event handler and start the timer
            cdc.TimeExpired += myHandler;
        }
        // Alert the user that the time has expired
        public void TimeExpired(object theClock, CountDownClockEventArgs e)
        {
            Console.WriteLine("logging " + e.message);

        }
    }
}
