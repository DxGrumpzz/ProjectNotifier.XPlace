namespace XPlace_ProjectNotifier
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Linq;
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


			IConfigurationRoot configurationBuilder = new ConfigurationBuilder()
			.AddJsonFile("Config.json", true, true)
			.Build();


			serviceCollection.AddSingleton(configurationBuilder);
			

			IServiceProvider provider = serviceCollection.BuildServiceProvider();

			var config = provider.GetService<IConfigurationRoot>();


			Current.MainWindow = new MainWindow(new MainWindowViewModel
			{
				Model = new MainWindowModel()
				{
					Title = "XPlace Project Notifier",
				},
			});
			Current.MainWindow.Show();
		}
	}
}
