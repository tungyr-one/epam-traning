using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                var config = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .Build();

                var servicesProvider = BuildDi(config);

                using (servicesProvider as IDisposable)
                {
                    var strConvert = servicesProvider.GetRequiredService<Converter.StrConverter>();
                    Console.WriteLine(strConvert.ConvertStrToInt("86483"));

                    Console.WriteLine("Press ANY key to exit");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }

            static IServiceProvider BuildDi(IConfiguration config)
            {
                return new ServiceCollection()
                   .AddTransient<Converter.StrConverter>()
                   .AddLogging(loggingBuilder =>
                   {
                        loggingBuilder.ClearProviders();
                       loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                       loggingBuilder.AddNLog(config);
                   })
                   .BuildServiceProvider();
            }
        }
    }
}
