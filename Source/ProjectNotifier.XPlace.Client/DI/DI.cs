namespace ProjectNotifier.XPlace.Core
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	/// Main DI Container class for this application
	/// </summary>
	public class DIContainer : IDI
	{
	
		/// <summary>
		/// Main provider
		/// </summary>
		public ServiceProvider Provider { get; private set; }


		/// <summary>
		/// Returns a service from the <see cref="Provider"/>
		/// </summary>
		/// <typeparam name="TService"> The type of service </typeparam>
		/// <returns></returns>
		public TService GetService<TService>()
		{
			return Provider.GetService<TService>();
		}

		/// <summary>
		/// Sets up the injecting service provider 
		/// </summary>
		/// <param name="provider"></param>
		public void SetupDI(ServiceProvider provider)
		{
			Provider = provider;
		}
	};


	/// <summary>
	/// A shortcut for the implementation of <see cref="DIContainer"/>
	/// </summary>
	public static class DI
	{
		
		#region Private fields

		private static DIContainer _diContainer;

		#endregion


		/// <summary>
		/// Main provider
		/// </summary>
		public static ServiceProvider Provider => _diContainer.Provider;


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
			(_diContainer = new DIContainer())
			// Setup the provider
			.SetupDI(provider);
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
		public static ClientAppSettingsModel ClientAppSettings()
		{
			return GetService<ClientAppSettingsModel>();
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
