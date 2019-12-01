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
        public async Task StartHubConnectionAsync()
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

        /// <summary>
        /// Login using a saved cookie 
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CookieLoginAsync(string cookie)
        {
            // Setup cookies
            CookieContainer.SetCookies(new Uri(ApiRoutes.API_URL), cookie);

            // Set login request
            var response = await _client.GetAsync($"{ApiRoutes.API_URL}/{ApiRoutes.API_ACCOUNT_CONTROLLER}/{ApiRoutes.API_ACCOUNT_CONTROLLER_LOGIN}");

            return response;
        }

        /// <summary>
        /// Login using normal user credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> LoginAsync(string username, SecureString password)
        {
            // Set login request
            var response = await _client.PostAsJsonAsync($"{ApiRoutes.API_URL}/{ApiRoutes.API_ACCOUNT_CONTROLLER}/{ApiRoutes.API_ACCOUNT_CONTROLLER_LOGIN}/{{LoginModel}}",
                new LoginRequestModel()
                {
                    Username = username,
                    Password = password.Unsecure(),
                });

            return response;
        }

        /// <summary>
        /// Send a registration request to the server
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="confirmationPassword"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> RegsiterAsync(string username, SecureString password, SecureString confirmationPassword)
        {
            // Send registration request
            var response = await _client.PostAsJsonAsync($"{ApiRoutes.API_URL}/{ApiRoutes.API_ACCOUNT_CONTROLLER}/{ApiRoutes.API_ACCOUNT_CONTROLLER_REGISTER}",
            new RegisterModel()
            {
                Username = username,
                Password = password.Unsecure(),
                ConfirmationPassword = confirmationPassword.Unsecure(),
            });

            return response;
        }


        /// <summary>
        /// Sends a request to the server that will update the user's project preferences
        /// </summary>
        /// <param name="newProjectPreferences"> The user's new preferences</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> UpdateUserPreferencesAsync(IEnumerable<ProjectType> newProjectPreferences)
        {
            // Send a request to update project preferences
            var updateProfileRequest = await _client
                .PostAsJsonAsync($"{ApiRoutes.API_URL}/{ApiRoutes.API_PROFILE_CONTROLLER}/{ApiRoutes.API_PROFILE_UPDATE_USER_PREFERENCES}/{{ProjectTypes}}", newProjectPreferences);

            return updateProfileRequest;
        }


    };
};