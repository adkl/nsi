using NSI.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSI.Common.Enumerations;
using NLog;
using NSI.Common.Exceptions;

namespace NSI.Logger.Implementations
{
    public class NLogAdapter : ILoggerAdapter
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public void LogException<T>(Exception ex, T request, SeverityEnum severity = SeverityEnum.Error)
        {
            _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
        }

        public void LogException<T>(Exception ex, SeverityEnum severity = SeverityEnum.Error)
        {
            _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
        }

        public void LogException<T>(NsiBaseException ex, T request)
        {
            _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, request);
        }

        public void LogException<T>(NsiBaseException ex)
        {
            _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, null);
        }

        /// <summary>
        /// Wrapper around NLog. Use this to log debug level messages. 
        /// </summary>
        /// <param name="message">Message to log</param>
        public void LogDebug(String message)
        {
            _logger.Debug(message);
        }

        private LogLevel ConvertToNLogLevel(SeverityEnum severity)
        {
            switch (severity)
            {
                case SeverityEnum.Info:
                    return LogLevel.Info;
                case SeverityEnum.Fatal:
                    return LogLevel.Fatal;
                case SeverityEnum.Warning:
                    return LogLevel.Warn;
                case SeverityEnum.Debug:
                    return LogLevel.Debug;
                default:
                    return LogLevel.Error;
            }
        }
    }
}
