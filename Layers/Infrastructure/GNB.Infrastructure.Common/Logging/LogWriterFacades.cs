using System;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using vm.Aspects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GNB.Infrastructure.Common.Logging
{
    public class LogWriterFacades
    {
        public const string General = "General";
        public const string Alert = "Alert";
        public const string Trace = "Trace";
        public const string Email = "Email";
        public const string Exception = "Exception";
        public const string EventLog = "Event Log";


        private LogWriter _logger;

        public LogWriterFacades()
        {
            _logger = LogConfigProvider.CreateLogWriter(LogConfigProvider.LogConfigurationFileName);
        }

        public void LogMessageAsync(LogEntry pLogEntry)
        {
            Task.Factory.StartNew(() => { _logger.Write(pLogEntry); });
        }

        public LogWriter WriteMessage(
            string aplicacion,
            string nivel,
            string titulo,
            string mensaje,
            Dictionary<string, object> properties,
            string category,
            TraceEventType severity)
        {
            if (!_logger.IsLoggingEnabled())
                return _logger;

            var entry = new LogEntry
            {
                AppDomainName = aplicacion,
                ProcessName = nivel,
                Title = titulo,
                Message = mensaje,
                Categories = new[] { category },
                Severity = severity
            };

            if (properties != null)
                entry.ExtendedProperties = properties;

            if (_logger.ShouldLog(entry))
            {
                LogMessageAsync(entry);
            }

            return _logger;
        }

        public LogWriter WriteMessage(
           string category,
           TraceEventType severity,
           Dictionary<string, object> properties,
           string format,
           params object[] args)
        {

            if (!_logger.IsLoggingEnabled())
                return _logger;

            var entry = new LogEntry
            {
                Categories = new[] { category },
                Severity = severity,
            };

            if (properties != null)
                entry.ExtendedProperties = properties;

            if (!_logger.ShouldLog(entry))
                return _logger;

            switch (args.Length)
            {
                case 0:
                    entry.Message = format;
                    break;

                case 1:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args[0]);
                    break;

                case 2:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args[0], args[1]);
                    break;

                case 3:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args[0], args[1], args[2]);
                    break;

                default:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args);
                    break;
            }

            LogMessageAsync(entry);

            return _logger;

        }

        public LogWriter WriteMessage(
           string category,
           TraceEventType severity,
           string format,
           params object[] args)
        {

            if (!_logger.IsLoggingEnabled())
                return _logger;

            var entry = new LogEntry
            {
                Categories = new[] { category },
                Severity = severity,
            };

            if (!_logger.ShouldLog(entry))
                return _logger;

            switch (args.Length)
            {
                case 0:
                    entry.Message = format;
                    break;

                case 1:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args[0]);
                    break;

                case 2:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args[0], args[1]);
                    break;

                case 3:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args[0], args[1], args[2]);
                    break;

                default:
                    entry.Message = string.Format(CultureInfo.InvariantCulture, format, args);
                    break;
            }

            LogMessageAsync(entry);

            return _logger;
        }

        public LogWriter WriteMessage(
            string category,
            TraceEventType severity,
            Func<string, object[], string> formatMessage,
            string format,
            params object[] args)
        {

            if (!_logger.IsLoggingEnabled())
                return _logger;

            var entry = new LogEntry
            {
                Categories = new[] { category },
                Severity = severity,
            };

            if (_logger.ShouldLog(entry))
            {
                entry.Message = formatMessage(format, args);
                LogMessageAsync(entry);
            }

            return _logger;
        }

        #region Sending messages to the general log
        public void LogMessage(
             TraceEventType severity,
             string format,
             params object[] args)
        {
            WriteMessage(General, severity, format, args);
        }

        public void LogInfo(
                string format,
                params object[] args)
        {

            LogMessage(TraceEventType.Information, format, args);
        }

        public void LogWarning(
              string format,
              params object[] args)
        {

            LogMessage(TraceEventType.Warning, format, args);
        }

        public void LogError(
              string format,
              params object[] args)
        {
            LogMessage(TraceEventType.Error, format, args);
        }

        public void LogCritical(
            string format,
            params object[] args)
        {
            LogMessage(TraceEventType.Critical, format, args);
        }
        #endregion

        #region Sending messages to the trace log
        public void TraceMessage(
               TraceEventType severity,
               string format,
               params object[] args)
        {
            WriteMessage(Trace, severity, format, args);
        }

        public void TraceInfo(
              string format,
              params object[] args)
        {
            TraceMessage(TraceEventType.Information, format, args);
        }

        public void TraceWarning(
          string format,
          params object[] args)
        {
            TraceMessage(TraceEventType.Warning, format, args);
        }

        public void TraceError(
            string format,
            params object[] args)
        {
            TraceMessage(TraceEventType.Error, format, args);
        }
        #endregion

        #region Sending messages to the alert log
        void AlertMessage(
         TraceEventType severity,
         string format,
         params object[] args)
        {

            WriteMessage(Alert, severity, format, args);
        }

        public void AlertInfo(
             string format,
             params object[] args)
        {

            AlertMessage(TraceEventType.Information, format, args);
        }

        public void AlertWarning(
             string format,
             params object[] args)
        {
            AlertMessage(TraceEventType.Warning, format, args);
        }

        public void AlertError(
            string format,
            params object[] args)
        {
            AlertMessage(TraceEventType.Error, format, args);
        }

        public void AlertCritical(
            string format,
            params object[] args)
        {
            AlertMessage(TraceEventType.Critical, format, args);
        }
        #endregion

        #region Sending messages via E-mail
        void EmailMessage(
               TraceEventType severity,
               string format,
               params object[] args)
        {
            WriteMessage(Email, severity, format, args);
        }

        public void EmailInfo(
               string format,
               params object[] args)
        {
            EmailMessage(TraceEventType.Information, format, args);
        }

        public void EmailWarning(
               string format,
               params object[] args)
        {
            EmailMessage(TraceEventType.Warning, format, args);
        }

        public void EmailError(
               string format,
               params object[] args)
        {
            EmailMessage(TraceEventType.Error, format, args);
        }

        public void EmailCritical(
               string format,
               params object[] args)
        {
            EmailMessage(TraceEventType.Critical, format, args);
        }
        #endregion

        #region Logging exceptions
        public void ExceptionMessage(
            TraceEventType severity,
            System.Exception exception)
        {
            WriteMessage(Exception, severity, (f, a) => exception.DumpString(), "dummy");
        }

        public void ExceptionInfo(
               System.Exception exception)
        {
            ExceptionMessage(TraceEventType.Information, exception);
        }


        public void ExceptionWarning(
            System.Exception exception)
        {
            ExceptionMessage(TraceEventType.Warning, exception);
        }


        public void ExceptionError(
            System.Exception exception)
        {
            ExceptionMessage(TraceEventType.Error, exception);
        }


        public void ExceptionCritical(
           System.Exception exception)
        {
            ExceptionMessage(TraceEventType.Critical, exception);
        }
        #endregion

        #region Sending messages to the Windows event log
        public void EventLogMessage(
              TraceEventType severity,
              string format,
              params object[] args)
        {

            WriteMessage(EventLog, severity, format, args);
        }

        public void EventLogInfo(
            string format,
            params object[] args)
        {

            EventLogMessage(TraceEventType.Information, format, args);
        }

        public void EventLogWarning(
             string format,
             params object[] args)
        {
            EventLogMessage(TraceEventType.Warning, format, args);
        }

        public void EventLogError(
               string format,
               params object[] args)
        {

            EventLogMessage(TraceEventType.Error, format, args);
        }

        public void EventLogCritical(
              string format,
              params object[] args)
        {

            EventLogMessage(TraceEventType.Critical, format, args);
        }
        #endregion

    }
}