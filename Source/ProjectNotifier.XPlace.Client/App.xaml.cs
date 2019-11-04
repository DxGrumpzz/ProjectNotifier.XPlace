namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectNotifier.XPlace.Core;
    using ProjectNotifier.XPlace.Relational;


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



        protected async override void OnStartup(StartupEventArgs e)
        {


            base.OnStartup(e);


            // Application setup stuff, DI build and such
            await ApplicationSetup();

            // Setup MainWindow
            (Current.MainWindow = new MainWindow(DI.GetService<MainWindowViewModel>()))
            // Show window
            .Show();
        }


        /// <summary>
        /// Setup application necessary "modules"
        /// </summary>
        /// <returns></returns>
        private async Task ApplicationSetup()
        {
            // Add services
            ServiceCollection serviceCollection = new ServiceCollection();

            // Main config
            serviceCollection.AddSingleton<IConfig>(new JsonConfig(AppFiles.ConfigFileName));


#if DEBUG == TRUE
            
            AllocConsole();
      
            // If in Debug attach console  logger
            serviceCollection.AddSingleton<ILoggerBase, ConsoleLogger>();
#else
			// If in Release attach file logger
			serviceCollection.AddSingleton<ILoggerBase>(new FileLogger());
#endif

            serviceCollection.AddSingleton((provider) =>
            new ClientAppSettingsModel(provider.GetService<IConfig>()));


            serviceCollection.AddSingleton<IUIManager, UIManager>();


            serviceCollection.AddSingleton((provider) =>
                new MainWindowViewModel()
                {
                    Model = new MainWindowModel()
                    {
                        Title = "XPlace Project Notifier",
                    },
                });


            serviceCollection.AddSingleton<IServerConnection, ServerConnection>();

            serviceCollection.AddSingleton<IClientCache, ClientCache>();

            // Add local data stores
            serviceCollection.AddScoped((provider) =>
            new ClientDataStoreDBContext(
                new DbContextOptionsBuilder<ClientDataStoreDBContext>()
                // Use SQL lite as the storage method
                .UseSqlite("Data Source = ProjectNotifier.XPlace.DataStore.db;")
                .Options));

            serviceCollection.AddScoped<IClientDataStore, ClientDataStore>((provider) =>
            new ClientDataStore(provider.GetService<ClientDataStoreDBContext>()));

            // Build provider
            DI.SetupDI(serviceCollection.BuildServiceProvider());

            // Ensure local data store is created
            await DI.GetService<IClientDataStore>().EnsureDataStoreCreatedAsync();
        }
    }
}