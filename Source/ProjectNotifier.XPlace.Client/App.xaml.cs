namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Numerics;
    using System.Threading;
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


            DI.Logger().Log("DI setup was completed succesfully", LogLevel.Informative);

            var settings = DI.ClientAppSettings();

            // If user requested to automatically login
            if (settings.RememberMe == true)
            {
                DI.Logger().Log("Logging in using auto sign-in", LogLevel.Informative);

                await LoginAsync(settings);
            };


            DI.Logger().Log("Initializing MainWindow", LogLevel.Informative);

            // Setup MainWindow
            (Current.MainWindow = new MainWindow(DI.GetService<MainWindowViewModel>()))
            // Show window
            .Show();


            DI.Logger().Log("MainWindow initialization complete", LogLevel.Informative);
        }


        /// <summary>
        /// Setup application necessary "modules"
        /// </summary>
        /// <returns></returns>
        private async Task ApplicationSetupAsync()
        {
            // Add services
            ServiceCollection serviceCollection = new ServiceCollection();


#if DEBUG == TRUE

            AllocConsole();

            // If in Debug attach console  logger
            serviceCollection.AddSingleton<ILoggerBase, ConsoleLogger>();
#else
            // If in Release attach file logger
            serviceCollection.AddSingleton<ILoggerBase>(new FileLogger());
#endif



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

            serviceCollection.AddTransient<ISignInManager,SignInManager>((provider) =>
            new SignInManager(provider.GetService<IServerConnection>()));


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
        private async Task LoginAsync(AppSettingsDataModel settings)
        {
            // Get local data store
            var dataStore = await DI.GetService<IClientDataStore>().GetLoginCredentialsAsync();

            if (dataStore is null)
            {
                settings.RememberMe = false;

                await DI.GetService<IClientDataStore>().SaveClientAppSettingsAsync();

                DI.Logger().Log("Auto sign-in failed, credentials store is empty", LogLevel.Verbose);
                return;
            };



            // Sign in using cookies
            await DI.GetService<ISignInManager>().CookieSignInAsync(dataStore.Cookie,
            signSuccessfull: async (response) =>
            {
                // Convert response to a list of projects
                var responseContent = await response.Content.ReadAsAsync<IEnumerable<ProjectModel>>();

                // Build hub connection
                await DI.GetService<IServerConnection>().StartHubConnectionAsync("Https://LocalHost:5001/ProjectsHub", DI.GetService<IServerConnection>().Cookies);

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
                        .Take(10))
                    },
                };

                DI.Logger().Log("Succesfully logged in", LogLevel.Informative);
            },
            signInFailed: async (response) =>
            {
                DI.Logger().Log($"Auto sign-in failed, Server error response {response.StatusCode}/{(int)response.StatusCode}", LogLevel.Verbose);

                settings.RememberMe = false;

                await DI.GetService<ClientDataStore>().SaveClientAppSettingsAsync();
            });
        }
    }
}