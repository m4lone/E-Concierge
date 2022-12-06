using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;

namespace qodeless.services.WebAPI
{
    public class Program
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
            try
            {
                Logger.Debug("API has started");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                throw;

            }
            finally
            {
                NLog.LogManager.Shutdown();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            });
    }
}
