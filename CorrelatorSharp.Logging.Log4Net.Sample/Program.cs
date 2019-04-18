using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using log4net.Config;
using log4net.Repository;
using log4net.Repository.Hierarchy;

namespace CorrelatorSharp.Logging.Log4Net.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var log4NetConfig = new XmlDocument();
            log4NetConfig.Load(File.OpenRead("log4net.config"));

            var repo = log4net.LogManager.CreateRepository(Assembly.GetExecutingAssembly(), typeof(Hierarchy));

            XmlConfigurator.Configure(repo, log4NetConfig["log4net"]);

            LoggingConfiguration.Current.UseLog4Net(repo);
            
            var logger = LogManager.GetLogger("Sample");

            using (ActivityScope.Create("Logging sample"))
            {
                logger.LogTrace("Trace Level log");
                logger.LogDebug("Debug Level log");
                logger.LogInfo("Info Level log");
                logger.LogWarn("Warn Level log");
                logger.LogError("Error Level log");
                logger.LogFatal("Fatal Level log");
            }

            using (ActivityScope.Create("Logging sample"))
            {
                await Task.WhenAll(
                    Task.Run(() => logger.LogTrace("Trace Level log")),
                    Task.Run(() => logger.LogDebug("Debug Level log")),
                    Task.Run(() =>
                    {
                        using (ActivityScope.Child("Child scope 1"))
                            logger.LogInfo("Info Level log");
                    }),
                    Task.Run(() => logger.LogWarn("Warn Level log")),
                    Task.Run(() =>
                    {
                        using (ActivityScope.Child("Child scope 2"))
                            logger.LogInfo("Error Level log");
                    }),
                    Task.Run(() => logger.LogFatal("Fatal Level log"))
                );
            }

            Console.ReadLine();
        }
    }
}
