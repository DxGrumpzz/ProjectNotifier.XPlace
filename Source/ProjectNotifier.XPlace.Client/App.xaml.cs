namespace ProjectNotifier.XPlace.Client
{
    using System.Windows;

    using Microsoft.Extensions.DependencyInjection;

    using ProjectNotifier.XPlace.Core;


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


        
            // Pre provider building objects.
            
            // Main config
            IConfig config = new JsonConfig(AppFiles.ConfigFileName);
            
            // Objects that *will* be used as the main instance for DI but are necessary for DI object initializtion
            var clientAppSettingsModel = new ClientAppSettingsModel(config);


            // Add services
            ServiceCollection serviceCollection = new ServiceCollection();


#if DEBUG == TRUE
            // If in Debug attach console  logger
            serviceCollection.AddSingleton<ILoggerBase, ConsoleLogger>();
#else
			// If in Release attach file logger
			serviceCollection.AddSingleton<ILoggerBase>(new FileLogger());
#endif

            serviceCollection.AddSingleton(clientAppSettingsModel);

            serviceCollection.AddSingleton(config);

            serviceCollection.AddSingleton<IUIManager, UIManager>();

            serviceCollection.AddSingleton(new MainWindowViewModel(clientAppSettingsModel)
            {
                Model = new MainWindowModel()
                {
                    Title = "XPlace Project Notifier",
                },
            });


            serviceCollection.AddSingleton<IServerConnection, ServerConnection>();

            serviceCollection.AddSingleton<IClientCache, ClientCache>();

            // Build provider
            DI.SetupDI(serviceCollection.BuildServiceProvider());


            // Setup MainWindow
            (Current.MainWindow = new MainWindow(DI.GetService<MainWindowViewModel>()))
            // Show window
            .Show();

        }
    }
}