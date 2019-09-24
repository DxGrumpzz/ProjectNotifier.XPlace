﻿namespace ProjectNotifier.XPlace.Client
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using ProjectNotifier.XPlace.Core;
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

			var clientAppSettingsModel = new ClientAppSettingsModel()
			{
				// Get number of project to display
				ProjectsToDisplay = Convert.ToInt32(configurationBuilder.GetSection(nameof(ClientAppSettingsModel.ProjectsToDisplay)).Value),

				// Get number of seconds to display notificaiton
				KeepNotificationOpenSeconds = Convert.ToInt32(configurationBuilder.GetSection(nameof(ClientAppSettingsModel.KeepNotificationOpenSeconds)).Value),
			};


			// Bind services
			ServiceCollection serviceCollection = new ServiceCollection();


			// If in Debug attach console  logger
#if DEBUG == TRUE

			serviceCollection.AddSingleton<ILoggerBase>(new ConsoleLogger());
#else
			// attach file logger
			serviceCollection.AddSingleton<ILoggerBase>(new FileLogger());
#endif

			serviceCollection.AddSingleton(configurationBuilder);

			serviceCollection.AddSingleton(clientAppSettingsModel);

			serviceCollection.AddSingleton(new JsonConfigManager(AppFiles.ConfigFileName));

			serviceCollection.AddSingleton(new ProjectLoader(TimeSpan.FromMinutes(10).TotalMilliseconds, clientAppSettingsModel));

			serviceCollection.AddSingleton<IUIManager>(new UIManager());


			// Build provider
			DI.SetupDI(serviceCollection.BuildServiceProvider());



			// Setup MainWindow
			(Current.MainWindow = new MainWindow(
			new MainWindowViewModel(DI.ClientAppSettings(), DI.GetService<ProjectLoader>())
			{
				Model = new MainWindowModel()
				{
					Title = "XPlace Project Notifier",
				},
			}))
			// Show window
			.Show();

		}
	}
}