using CorrelatorSharp.Logging.Log4Net;
using log4net.Repository;

namespace CorrelatorSharp.Logging
{
    public static class LoggingConfigurationExtensions
    {
        public static LoggingConfiguration UseLog4Net(this LoggingConfiguration config, ILoggerRepository repo = null)
        {
            config.WithLogManager(new LogManagerAdaptor(repo));
            return config;
        }
    }
}
