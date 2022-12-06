using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog;
using Microsoft.Extensions.Logging;
using System;

namespace qodeless.presentation.UI.Web
{
    public class Program
    {
        private static Logger Logger = LogManager.GetCurrentClassLogger();
        public static void Main(string[] args)
        {
           
            try
            {
                Logger.Debug("Portal has started");
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
