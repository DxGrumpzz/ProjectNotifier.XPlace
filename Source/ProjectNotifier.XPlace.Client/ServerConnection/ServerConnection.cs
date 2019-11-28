namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Security;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;
    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// An implementation of <see cref="IServerConnection"/>
    /// </summary>
    public class ServerConnection : IServerConnection
    {

        #region Public proeprties

        /// <summary>
        /// The client's Projects hub connection
        /// </summary>
        public HubConnection ProjectsHubConnection { get; set; }


        /// <summary>
        /// The HTTP connection to the server
        /// </summary>
        public HttpClient Client { get; set; }


        /// <summary>
        /// A cookie "jar" that contains information about the connection, user authorization, and more
        /// </summary>
        public CookieContainer Cookies { get; private set; }


        /// <summary>
        /// A client handler for cookie storage
        /// </summary>
        public HttpClientHandler ClientHandler { get; set; }

        #endregion


        public ServerConnection()
        {
            Cookies = new CookieContainer();

            ClientHandler = new HttpClientHandler()
            {
                CookieContainer = Cookies,
            };

            Client = new HttpClient(ClientHandler);
        }


        /// <summary>
        /// Build and start a connection to server hub
        /// </summary>
        /// <param name="url"> The url for the hub's connection </param>
        /// <param name="cookies"> A cookie used as a form of authorization </param>
        /// <returns></returns>
        public async Task StartHubConnectionAsync(string url, CookieContainer cookies)
        {
            // Build hub connection
            await (ProjectsHubConnection = new HubConnectionBuilder()
            // Connect to project hub url
            .WithUrl(url,
            // Authorize user with cookies
            options => options.Cookies = cookies)
            // Build hub connection
            .Build())
            // Start the connection
            .StartAsync();
        }


        public async Task<HttpResponseMessage> CookieLoginAsync(string cookie)
        {
            Cookies.SetCookies(new Uri("https://localhost:5001"), cookie);

            var response = await Client.GetAsync($"https://localhost:5001/Account/Login");

            return response;
        }

        public async Task<HttpResponseMessage> LoginAsync(string username, SecureString password)
        {
            var response = await Client.PostAsJsonAsync("https://localhost:5001/Account/Login/{LoginModel}",
                new LoginRequestModel()
                {
                    Username = username,
                    Password = password.Unsecure(),
                });

            return response;
        }

        public async Task<HttpResponseMessage> RegsiterAsync(string username, SecureString password, SecureString confirmationPassword)
        {
            var response = await DI.GetService<IServerConnection>().Client
            .PostAsJsonAsync("https://localhost:5001/Account/Register",
            new RegisterModel()
            {
                Username = username,
                Password = password.Unsecure(),
                ConfirmationPassword = confirmationPassword.Unsecure(),
            });

            return response;
        }

        public async Task<HttpResponseMessage> UpdateUserPreferencesAsync(IEnumerable<ProjectType> newProjectPreferences)
        {
            var updateProfileRequest = await Client
                    .PostAsJsonAsync("Https://localhost:5001/Profile/UpdateUserPreferences/{ProjectType}", newProjectPreferences);

            return updateProfileRequest;
        }
    };
};