namespace ProjectNotifier.XPlace.Core
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

	
    /// <summary>
    /// Main DI Container class for this application
    /// </summary>
    public class DI : IDI
	{

        /// <summary>
        /// Main provider
        /// </summary>
        public static ServiceProvider Provider { get; private set; }


        private DI() { }

        /// <summary>
        /// Returns a service from the <see cref="Provider"/>
        /// </summary>
        /// <typeparam name="TService"> The type of service </typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
		{
			return Provider.GetService<TService>();
		}


		/// <summary>
		/// Sets up the injecting service provider 
		/// </summary>
		/// <param name="provider"></param>
		public static void SetupDI(ServiceProvider provider)
		{
            // Instanciate an instnace of the dependency injection container
            Provider = provider;

        }


		#region Public helpers/shortcuts

		/// <summary>
		/// Returns the injected main configuration
		/// </summary>
		/// <returns></returns>
		public static IConfigurationRoot GetConfigurationRoot()
		{
			return GetService<IConfigurationRoot>();
		}


		/// <summary>
		/// Returns a logger
		/// </summary>
		/// <returns> Returns an instance of type <see cref="ILoggerBase"/> </returns>
		public static ILoggerBase Logger()
		{
			return GetService<ILoggerBase>();
		}


		/// <summary>
		/// Returns the main settings model
		/// </summary>
		/// <returns></returns>
		public static AppSettingsDataModel ClientAppSettings()
		{
			return GetService<IClientDataStore>().GetClientAppSettings();
		}


		/// <summary>
		/// Returns an <see cref="IUIManager"/> service
		/// </summary>
		/// <returns></returns>
		public static IUIManager UIManager()
		{
			return GetService<IUIManager>();
		}

        public static IProjectLoader ProjectLoader()
        {
            return GetService<IProjectLoader>();
        }

        #endregion
    }
};
