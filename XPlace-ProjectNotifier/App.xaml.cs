namespace XPlace_ProjectNotifier
{
	using Microsoft.Extensions.Configuration;
	using Microsoft.Extensions.DependencyInjection;
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
    using System.Threading.Tasks;
	using System.Windows;

	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);


			ServiceCollection serviceCollection = new ServiceCollection();

			// Bind services
			var configurationBuilder = new ConfigurationBuilder()
			.AddJsonFile("Config.json", false, true)
			.Build();

			serviceCollection.AddSingleton(configurationBuilder);


			serviceCollection.AddSingleton(new SettingsModel()
			{
				// Get number of project to display
				ProjectsToDisplay = Convert.ToInt32(configurationBuilder.GetSection("ProjectsToDisplay").Value),
			});

			serviceCollection.AddSingleton(new JsonConfigManager("Config.json"));

			// Build provider
			DI.SetupDI(serviceCollection.BuildServiceProvider());


			// Setup MainWindow
			(Current.MainWindow = new MainWindow(new MainWindowViewModel(DI.GetSettings())
			{
				Model = new MainWindowModel()
				{
					Title = "XPlace Project Notifier",
				},
			}))
			.Show();
		}
	}
}
