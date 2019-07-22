using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;

namespace GNB.Infrastructure.Common
{
    class UnityContainerComponent : IContainer
    {
        #region Default constants

        public const string DefaultConfigurationFileName = "unity.config";

        public const string DefaultConfigurationSectionName = "unity";

        public const string DefaultContainerName = "all";

        #endregion

        #region Fields

        IUnityContainer _root = new UnityContainer();

        volatile bool _isInitialized;

        #endregion

        #region Métodos de implementación de la interfaz IContainer

        public void InitializeAll()
        {
            Initialize();
        }

        public TService Resolve<TService>()
        {
            return _root.Resolve<TService>();
        }

        #endregion

        #region Métodos del Proveedor

        public IUnityContainer Initialize(
            string configFileName = DefaultConfigurationFileName,
            string configSection = DefaultConfigurationSectionName,
            string containerName = DefaultContainerName,
            Func<string, string, UnityConfigurationSection> getConfigFileSection = null)
        {

            if (_isInitialized)
                return _root;

            lock (_root)
            {
                if (_isInitialized)
                    return _root;

                try
                {
                    var unityConfigSection = getConfigFileSection != null
                                                ? getConfigFileSection(configFileName, configSection)
                                                : GetUnityConfigurationSection(configFileName, configSection);

                    // file was not specified or does not exist - try loading from web/app.config then
                    if (unityConfigSection == null)
                        unityConfigSection = ConfigurationManager.GetSection(configSection) as UnityConfigurationSection;

                    if (unityConfigSection != null)
                        if (string.IsNullOrWhiteSpace(containerName))
                            _root.LoadConfiguration(unityConfigSection);
                        else
                            _root.LoadConfiguration(unityConfigSection, containerName);

                    _isInitialized = true;

                    // initialize the CSL with Unity service location
                    if (!ServiceLocator.IsLocationProviderSet)
                        ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(_root));

                    return _root;
                }
                catch (System.Exception x)
                {
                    if (_root != null)
                        _root.DebugDump();

                    throw;
                }
            }
        }

        UnityConfigurationSection GetUnityConfigurationSection(
            string configFileName,
            string configSection)
        {
            // if specified, try loading from file (e.g. DIContainer.config)
            if (string.IsNullOrWhiteSpace(configFileName))
                return null;

            string fullConfigPathName;

            if (Path.IsPathRooted(configFileName))
                fullConfigPathName = configFileName;
            else
            {
                var data = AppDomain
                               .CurrentDomain
                               .GetData("APP_CONFIG_FILE");

                var path = Path.GetDirectoryName(data.ToString());

                fullConfigPathName = Path.Combine(path, configFileName);
            }

            var configuration = ConfigurationManager.OpenMappedExeConfiguration(
                                                        new ExeConfigurationFileMap { ExeConfigFilename = fullConfigPathName },
                                                        ConfigurationUserLevel.None);

            return configuration.GetSection(configSection) as UnityConfigurationSection;
        }

        #endregion
    }

    public static class ExtUnity
    {
        /// <summary>
        /// Dumps the contents of a container as a text to the VS output pane.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public static void DebugDump(
            this IUnityContainer container)
        {

            using (var writer = new StringWriter(CultureInfo.InvariantCulture))
            {
                container.Dump(writer);
                Debug.WriteLine(
                    "==============================={1}" +
                    "{0}{1}" +
                    "==============================={1}",
                    writer.GetStringBuilder(),
                    writer.NewLine);
            }
        }

        /// <summary>
        /// Dumps the contents of a container as a text to a <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="writer">The text writer to dump the container to.</param>
        public static void Dump(
            this IUnityContainer container,
            TextWriter writer)
        {
            var registrations = container.Registrations;

            writer.WriteLine("Container has {0} Registrations:", registrations.Count());

            foreach (var item in registrations
                                    .OrderBy(i => i.RegisteredType.Name)
                                    .ThenBy(i => i.MappedToType.Name)
                                    .ThenBy(i => i.Name))
            {
                var regType = item.RegisteredType.Name;
                var mapTo = item.MappedToType.Name;
                var regName = item.Name ?? "[default]";
                var lifetime = item.LifetimeManagerType.Name;

                if (mapTo != regType)
                    mapTo = " -> " + mapTo;
                else
                    mapTo = string.Empty;
                lifetime = lifetime.Substring(0, lifetime.Length - "LifetimeManager".Length);

                writer.WriteLine("+ {0}{1}  '{2}'  {3}", regType, mapTo, regName, lifetime);
            }
        }
    }
}