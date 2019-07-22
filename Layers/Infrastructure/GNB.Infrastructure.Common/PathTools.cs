using System;
using System.Configuration;
using System.IO;

namespace GNB.Infrastructure.Common
{
    public static partial class PathTools
    {
        public static ConfigurationSection GetConfigurationSection(
           string pConfigFileName,
           string pConfigSection)
        {
            if (string.IsNullOrWhiteSpace(pConfigFileName))
                return null;

            string fullConfigPathName;

            if (Path.IsPathRooted(pConfigFileName))
                fullConfigPathName = pConfigFileName;
            else
            {
                var data = AppDomain
                               .CurrentDomain
                               .GetData("APP_CONFIG_FILE");

                var path = Path.GetDirectoryName(data.ToString());

                fullConfigPathName = Path.Combine(path, pConfigFileName);
            }

            var configuration = ConfigurationManager.OpenMappedExeConfiguration(
                                                        new ExeConfigurationFileMap { ExeConfigFilename = fullConfigPathName },
                                                        ConfigurationUserLevel.None);

            return configuration.GetSection(pConfigSection);
        }

        public static string GetPath(string pConfigFileName = null)
        {


            var data = AppDomain
                           .CurrentDomain
                           .GetData("APP_CONFIG_FILE");

            var path = Path.GetDirectoryName(data.ToString());

            if (string.IsNullOrEmpty(pConfigFileName))
                return path;
            else
                return Path.Combine(path, pConfigFileName);

        }
    }
}
