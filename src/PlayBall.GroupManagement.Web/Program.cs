using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using LogLevel = NLog.LogLevel;

namespace PlayBall.GroupManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("#######starting app########");
            ConfigureNLog();
            CreateHostBuilder(args)
                .Build()
                .Run();
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureLogging(logging =>
                {
                    // clear default providers, nlog handle it
                    logging.ClearProviders();

                    // set minimum level to trace, nlog rules will kick in afterwards
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  // NLog: setup NLog for Dependency injection;

        // TODO: replace with nlog.config
        private static void ConfigureNLog()
        {
            var config =
                new LoggingConfiguration();

            var consoleTarget =
                 new ColoredConsoleTarget("coloredConsole")
                 {
                     Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
                 };

            config.AddTarget(consoleTarget);

            config.AddRule(LogLevel.Trace, LogLevel.Fatal, consoleTarget, "PlayBall.GroupManagement.Web.IoC.*");
            config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);

            LogManager.Configuration = config;
        }
    }

}
