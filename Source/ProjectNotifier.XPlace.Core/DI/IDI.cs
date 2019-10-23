namespace ProjectNotifier.XPlace.Core
{
	using Microsoft.Extensions.DependencyInjection;


	/// <summary>
	/// Specifies the implementation of a Dependency injection provider
	/// </summary>
	public interface IDI
	{

		/// <summary>
		/// Main provider
		/// </summary>
		public static ServiceProvider Provider { get; }


		/// <summary>
		/// Returns a service from the <see cref="Provider"/>
		/// </summary>
		/// <typeparam name="TService"> The type of service </typeparam>
		/// <returns></returns>
		public static TService GetService<TService>()
        { 
            return default; 
        }


		/// <summary>
		/// Sets up the injecting service provider 
		/// </summary>
		/// <param name="provider"></param>
		public static void SetupDI(ServiceProvider provider)
        {
        }
	};
};