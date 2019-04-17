using System;
using log4net;
using log4net.Core;

namespace CorrelatorSharp.Logging.Log4Net
{
    public class LoggerAdaptor : CorrelatorSharp.Logging.ILogger
    {
        public const string ActivityIdPropertyName = "cs-activity-id";
        public const string ParentIdPropertyName = "cs-activity-parentid";
        public const string NamePropertyName = "cs-activity-name";

        private readonly ILog _logger;
        private readonly Type _type;

        public LoggerAdaptor(log4net.ILog logger)
        {
            _logger = logger;
            _type = logger.GetType();
        }
        
        public string Name => _logger.Logger.Name;

        public bool IsTraceEnabled => _logger.Logger.IsEnabledFor(Level.Trace);
        public bool IsWarnEnabled => _logger.IsWarnEnabled;
        public bool IsInfoEnabled => _logger.IsInfoEnabled;
        public bool IsErrorEnabled => _logger.IsErrorEnabled;
        public bool IsFatalEnabled => _logger.IsFatalEnabled;
        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        public void AddCorrelationProperties()
        {
            var currentActivityScope = ActivityScope.Current;
            if (currentActivityScope == null)
            {
                return;
            }

            log4net.LogicalThreadContext.Properties[ActivityIdPropertyName] = currentActivityScope.Id;
            log4net.LogicalThreadContext.Properties[NamePropertyName] = currentActivityScope.Name;
            log4net.LogicalThreadContext.Properties[ParentIdPropertyName] = currentActivityScope.ParentId;
        }
        
        public void LogTrace(Exception exception, string format = "", params object[] values)
        {
            if (IsTraceEnabled)
            {
                AddCorrelationProperties();
                _logger.Logger.Log(_type, Level.Trace, string.Format(format, values), exception);
            }
        }

        public void LogDebug(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsDebugEnabled)
            {
                AddCorrelationProperties();
                _logger.Debug(string.Format(format, values), exception);
            }
        }

        public void LogInfo(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsInfoEnabled)
            {
                AddCorrelationProperties();
                _logger.Info(string.Format(format, values), exception);
            }
        }

        public void LogWarn(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsWarnEnabled)
            {
                AddCorrelationProperties();
                _logger.Warn(string.Format(format, values), exception);
            }
        }

        public void LogError(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsErrorEnabled)
            {
                AddCorrelationProperties();
                _logger.Error(string.Format(format, values), exception);
            }
        }

        public void LogFatal(Exception exception, string format = "", params object[] values)
        {
            if (_logger.IsFatalEnabled)
            {
                AddCorrelationProperties();
                _logger.Fatal(string.Format(format, values), exception);
            }
        }

        public void LogTrace(string format, params object[] values)
        {
            if (IsTraceEnabled)
            {
                AddCorrelationProperties();
                _logger.Logger.Log(_type, Level.Trace, string.Format(format, values), null);
            }
        }

        public void LogDebug(string format, params object[] values)
        {
            if (_logger.IsDebugEnabled)
            {
                AddCorrelationProperties();
                _logger.Debug(string.Format(format, values));
            }
        }

        public void LogInfo(string format = "", params object[] values)
        {
            if (_logger.IsInfoEnabled)
            {
                AddCorrelationProperties();
                _logger.Info(string.Format(format, values));
            }
        }

        public void LogWarn(string format = "", params object[] values)
        {
            if (_logger.IsWarnEnabled)
            {
                AddCorrelationProperties();
                _logger.Warn(string.Format(format, values));
            }
        }

        public void LogError(string format = "", params object[] values)
        {
            if (_logger.IsErrorEnabled)
            {
                AddCorrelationProperties();
                _logger.Error(string.Format(format, values));
            }
        }

        public void LogFatal(string format = "", params object[] values)
        {
            if (_logger.IsFatalEnabled)
            {
                AddCorrelationProperties();
                _logger.Fatal(string.Format(format, values));
            }
        }
    }
}
