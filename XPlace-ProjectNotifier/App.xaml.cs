namespace XPlace_ProjectNotifier
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Windows;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

#if DEBUG == TRUE

		/// <summary>
		/// Creates a console window, Mainly used to display console log output
		/// </summary>
		/// <returns></returns>
		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		private static extern bool AllocConsole();
#endif

		protected override void OnStartup(StartupEventArgs e)
		{

#if DEBUG == TRUE
			AllocConsole();
#endif

			base.OnStartup(e);

			var configurationBuilder = new ConfigurationBuilder()
			.AddJsonFile(AppFiles.ConfigFileName, false, true)
			.Build();

			// Bind services
			ServiceCollection serviceCollection = new ServiceCollection();

			serviceCollection.AddSingleton<ILoggerBase>(new ConsoleLogger());

			serviceCollection.AddSingleton(configurationBuilder);

			serviceCollection.AddSingleton(new SettingsModel()
			{
				// Get number of project to display
				ProjectsToDisplay = Convert.ToInt32(configurationBuilder.GetSection("ProjectsToDisplay").Value),
			});

			serviceCollection.AddSingleton(new JsonConfigManager(AppFiles.ConfigFileName));


			// Build provider
			DI.SetupDI(serviceCollection.BuildServiceProvider());


			DI.GetLogger().Log($"Creating main window");

			// Setup MainWindow
			(Current.MainWindow = new MainWindow(new MainWindowViewModel(DI.GetSettings())
			{
				Model = new MainWindowModel()
				{
					Title = "XPlace Project Notifier",
				},
			}))
			.Show();

			DI.GetLogger().Log($"Displaying main window");
		}
	}
}
