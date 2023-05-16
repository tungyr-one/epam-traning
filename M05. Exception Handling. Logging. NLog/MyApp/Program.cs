using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
//https://github.com/NLog/NLog/wiki/Getting-started-with-.NET-Core-2---Console-application

namespace MyApp
{
    class Program
    {
        //private static Logger Logger = LogManager.GetCurrentClassLogger();
        //private static Logger Logger = LogManager.GetLogger("MyFancyLogger");

        static void Main(string[] args)
        {

            //var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            //var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            //var logConsoleAllLevels = new NLog.Targets.ColoredConsoleTarget("logConsoleAll");
            //logConsoleAllLevels.Layout = "[${longdate}] [${uppercase:${level}}] [${logger:shortName=true}] ${message} ${exception:format=tostring}";
       
            //var logConsoleErrorLevel = new NLog.Targets.ColoredConsoleTarget("logConsoleError");
            //logConsoleAllLevels.Layout = "[${longdate}] [${uppercase:${level}}] [${logger:shortName=true}] ${message} ${exception:format=tostring}";

            //config.AddRule(minLevel: LogLevel.Trace, LogLevel.Warn, logConsoleAllLevels);
            //config.AddRuleForOneLevel(LogLevel.Error, logConsoleErrorLevel);
            //// Apply config           
            //LogManager.Configuration = config;

            //Logger.Info("Main started");

            //Logger.Trace("Trace message");
            //Logger.Debug("Debug message");
            
            //Logger.Warn("Warn message");
            //Logger.Error("Error message");
            //Logger.Fatal("Fatal message");
            //Console.WriteLine("\n", 5);

            //try
            //{
            //    Logger.Info("Hello world");
            //    throw new Exception();
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error(ex, "Goodbye cruel world");
            //}

            //Console.WriteLine(Converter.StrConverter.ConvertStrToInt("ksdfgh"));

            //----------------------------------

            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory()) //From NuGet Package Microsoft.Extensions.Configuration.Json
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();

                var servicesProvider = BuildDi(config);

                using (servicesProvider as IDisposable)
                {
                    //    var runner = servicesProvider.GetRequiredService<Runner>();
                    //    runner.DoAction("Action1");

                    var strConvert = servicesProvider.GetRequiredService<Converter.StrConverter>();
                    strConvert.ConvertStrToInt("84a654");


                    Console.WriteLine("Press ANY key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }

            //try
            //{
            //    logger.Info("Hello world");
            //}
            //catch (Exception ex)
            //{
            //    logger.Error(ex, "Goodbye cruel world");
            //}

            //Console.WriteLine(Converter.StrConverter.ConvertStrToInt("ksdfgh"));


            static IServiceProvider BuildDi(IConfiguration config)
            {
                return new ServiceCollection()
                   //.AddTransient<Runner>() // Runner is the custom class
                   .AddTransient<Converter.StrConverter>() // Runner is the custom class
                   .AddLogging(loggingBuilder =>
                   {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                       loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                       loggingBuilder.AddNLog(config);
                   })
                   .BuildServiceProvider();
            }
        }
    }

    //public class Runner
    //{
    //    private readonly ILogger<Runner> _logger;

    //    public Runner(ILogger<Runner> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public void DoAction(string name)
    //    {
    //        _logger.LogDebug(20, "Doing hard work! {Action}", name);
    //    }
    
    //}


}
