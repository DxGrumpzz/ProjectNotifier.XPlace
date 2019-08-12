namespace XPlace_ProjectNotifier
{
	using System;
	using Microsoft.Extensions.DependencyInjection;

	/// <summary>
	/// Main DI class for this application
	/// </summary>
	public static class DI
	{
		public static ServiceProvider Provider;

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
			Provider = provider;
		}
	}
}
