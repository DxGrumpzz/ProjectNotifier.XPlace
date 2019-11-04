namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows;
    using Microsoft.AspNetCore.SignalR.Client;
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
            await ApplicationSetupAsync();

            var settings = DI.ClientAppSettings();

            // If user requested to automatically login
            if (settings.RememberMe == true)
            {
                await LoginAsync(settings);
            };


            // Setup MainWindow
            (Current.MainWindow = new MainWindow(DI.GetService<MainWindowViewModel>()))
            // Show window
            .Show();
        }


        /// <summary>
        /// Setup application necessary "modules"
        /// </summary>
        /// <returns></returns>
        private async Task ApplicationSetupAsync()
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

        /// <summary>
        /// Use a cookie to auto login
        /// </summary>
        /// <returns></returns>
        private async Task LoginAsync(ClientAppSettingsModel settings)
        {
            // Get local data store
            var dataStore = await DI.GetService<IClientDataStore>().GetLoginCredentialsAsync();

            // Set the cookies
            DI.GetService<IServerConnection>().Cookies.SetCookies(new Uri("https://localhost:5001"), dataStore.Cookie);

            // Get project list
            var response = await DI.GetService<IServerConnection>().Client.GetAsync($"https://localhost:5001/Projects");

            // Convert response to a list of projects
            var responseContent = await response.Content.ReadAsAsync<IEnumerable<ProjectModel>>();

            // Build hub connection
            await (DI.GetService<IServerConnection>().ProjectsHubConnection =
            new HubConnectionBuilder()
            // Connect to project hub url
            .WithUrl("Https://LocalHost:5001/ProjectsHub", options =>
            {
                // Authorize user with cookies
                options.Cookies = DI.GetService<IServerConnection>().Cookies;
            })
            // Build hub connection
            .Build())
            // Start the connection
            .StartAsync();


            // Update cache
            DI.GetService<IClientCache>().ProjectListCache = responseContent;


            // Change to projects view
            DI.GetService<MainWindowViewModel>().CurrentPage = new ProjectsPageView()
            {
                ViewModel = new ProjectsPageViewModel()
                {
                    // Convert the list of IEnumerable to an ObservableCollection
                    ProjectList = new ObservableCollection<ProjectItemViewModel>(responseContent
                    .Select((p) => new ProjectItemViewModel()
                    {
                        ProjectModel = p,
                    })
                    .Take(settings.ProjectsToDisplay))
                },
            };
        }
    }
}