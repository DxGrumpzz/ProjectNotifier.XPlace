namespace XPlace_ProjectNotifier
{
	using System;
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	/// Main DI class for this application
	/// </summary>
	public static class DI
	{
		/// <summary>
		/// Main provider
		/// </summary>
		public static ServiceProvider Provider { get; private set; } 


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
		/// Returns the injected main configuration
		/// </summary>
		/// <returns></returns>
		public static IConfigurationRoot GetConfigurationRoot()
		{
			return GetService<IConfigurationRoot>();
		}


		/// <summary>
		/// Returns the main settings model
		/// </summary>
		/// <returns></returns>
		public static SettingsModel GetSettings()
		{
			return GetService<SettingsModel>();
		}


		/// <summary>
		/// Sets up the injecting service provider 
		/// </summary>
		/// <param name="provider"></param>
		public static void SetupDI(ServiceProvider provider)
		{
			Provider = provider;
		}
	}
}
