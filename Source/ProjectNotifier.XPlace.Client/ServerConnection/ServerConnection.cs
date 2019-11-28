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

        #region Private fields

        /// <summary>
        /// The HTTP connection to the server
        /// </summary>
        private readonly HttpClient _client;


        /// <summary>
        /// A client handler for cookie storage
        /// </summary>
        private readonly HttpClientHandler _clientHandler;

        #endregion


        #region Public properties
   
        /// <summary>
        /// A cookie "jar" that contains information about the connection, user authorization, and more
        /// </summary>
        public CookieContainer CookieContainer { get; }

        /// <summary>
        /// The client's Projects hub connection
        /// </summary>
        public HubConnection ProjectsHub { get; private set; }

        #endregion


        public ServerConnection()
        {
            CookieContainer = new CookieContainer();

            _clientHandler = new HttpClientHandler()
            {
                CookieContainer = CookieContainer,
            };

            _client = new HttpClient(_clientHandler);
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
            await (ProjectsHub = new HubConnectionBuilder()
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
            CookieContainer.SetCookies(new Uri("https://localhost:5001"), cookie);

            var response = await _client.GetAsync($"https://localhost:5001/Account/Login");

            return response;
        }

        public async Task<HttpResponseMessage> LoginAsync(string username, SecureString password)
        {
            var response = await _client.PostAsJsonAsync("https://localhost:5001/Account/Login/{LoginModel}",
                new LoginRequestModel()
                {
                    Username = username,
                    Password = password.Unsecure(),
                });

            return response;
        }

        public async Task<HttpResponseMessage> RegsiterAsync(string username, SecureString password, SecureString confirmationPassword)
        {
            var response = await _client.PostAsJsonAsync("https://localhost:5001/Account/Register",
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
            var updateProfileRequest = await _client
                .PostAsJsonAsync("Https://localhost:5001/Profile/UpdateUserPreferences/{ProjectType}", newProjectPreferences);

            return updateProfileRequest;
        }



    };
};