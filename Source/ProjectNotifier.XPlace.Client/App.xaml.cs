namespace ProjectNotifier.XPlace.Client
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Net.Http;
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

            // Pre provider building objects.
            // Objects that *will* be used as the main instance for DI but are necessary for DI object initializtion
            var clientAppSettingsModel = new ClientAppSettingsModel()
            {
                // Get number of project to display
                ProjectsToDisplay = Convert.ToInt32(configurationBuilder.GetSection(nameof(ClientAppSettingsModel.ProjectsToDisplay)).Value),

                // Get number of seconds to display notificaiton
                KeepNotificationOpenSeconds = Convert.ToInt32(configurationBuilder.GetSection(nameof(ClientAppSettingsModel.KeepNotificationOpenSeconds)).Value),
            };

            // Bind services
            ServiceCollection serviceCollection = new ServiceCollection();


#if DEBUG == TRUE
            // If in Debug attach console  logger
            serviceCollection.AddSingleton<ILoggerBase>(new ConsoleLogger());
#else
			// If in Release attach file logger
			serviceCollection.AddSingleton<ILoggerBase>(new FileLogger());
#endif

            serviceCollection.AddSingleton(configurationBuilder);

            serviceCollection.AddSingleton(clientAppSettingsModel);

            serviceCollection.AddSingleton(new JsonConfigManager(AppFiles.ConfigFileName));

            serviceCollection.AddSingleton<IUIManager>(new UIManager());

            serviceCollection.AddSingleton(new MainWindowViewModel(clientAppSettingsModel)
            {
                Model = new MainWindowModel()
                {
                    Title = "XPlace Project Notifier",
                },
            });

            serviceCollection.AddSingleton(new HttpClient());

            serviceCollection.AddSingleton<IServerConnection>(new ServerConnection());

            // Build provider
            DI.SetupDI(serviceCollection.BuildServiceProvider());


            // Setup MainWindow
            (Current.MainWindow = new MainWindow(DI.GetService<MainWindowViewModel>()))
            // Show window
            .Show();

        }
    }
}