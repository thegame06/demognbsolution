using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;

namespace GNB.Infrastructure.Common.Logging
{
    public static class LogConfigProvider
    {
        public static string LogConfigurationFileName = string.Empty;

        public static string FileName = "EntLib.config";

        static string _logConfigurationFileName;
        static IConfigurationSource _logConfiguration;


        public static LogWriter CreateLogWriterFromConfigFile(
            string configFileName)
        {
            if (string.IsNullOrWhiteSpace(configFileName))
                configFileName = PathTools.GetPath();

            _logConfigurationFileName = !string.IsNullOrWhiteSpace(configFileName) && File.Exists(Path.Combine(configFileName, FileName))
                                            ? Path.Combine(configFileName, FileName)
                                            : Path.Combine(PathTools.GetPath(), FileName);

            try
            {
                var logConfigSource = new FileConfigurationSource(_logConfigurationFileName);
                var logger = new LogWriterFactory(logConfigSource).Create();

                return logger;
            }
            catch (System.Exception x)
            {
                if (_logConfiguration != null)
                    _logConfiguration.Dispose();

                throw new ConfigurationErrorsException(
                            string.Format(
                                    CultureInfo.InvariantCulture,
                                    "There was an error loading the configuration from {0}.",
                                    string.IsNullOrWhiteSpace(configFileName) ? "the system configuration file" : Path.Combine(configFileName, FileName)),
                            x);
            }
        }

        public static LogWriter CreateLogWriter(
            string configFileName)
        {
            LogWriter logWriter;

            try
            {
                DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());

                logWriter = CreateLogWriterFromConfigFile(configFileName);
            }
            catch (ConfigurationErrorsException)
            {
                logWriter = CreateLogWriterFromConfigFile(configFileName);
            }


            Logger.Reset();
            Logger.SetLogWriter(logWriter);
            return logWriter;
        }

    }
}
