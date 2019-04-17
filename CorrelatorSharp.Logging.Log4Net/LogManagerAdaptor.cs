using System.Reflection;
using log4net.Repository;

namespace CorrelatorSharp.Logging.Log4Net
{
    public class LogManagerAdaptor : ILogManagerAdaptor
    {
        private static readonly Assembly ExecutingAssembly = Assembly.GetExecutingAssembly();
        private readonly string _repoName;

        public LogManagerAdaptor(ILoggerRepository repo)
        {
            _repoName = repo?.Name;
        }

        public ILogger GetLogger(string name)
        {
            if (string.IsNullOrEmpty(_repoName))
            {
                return new LoggerAdaptor(log4net.LogManager.GetLogger(ExecutingAssembly, name));
            }

            return new LoggerAdaptor(log4net.LogManager.GetLogger(_repoName, name));
        }
    }
}
