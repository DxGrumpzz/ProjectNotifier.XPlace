namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Security;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;

    /// <summary>
    /// An interface that holds a "list" of hub connection available to the client
    /// </summary>
    public interface IServerConnection
    {
        public HubConnection ProjectsHub { get; }
        public CookieContainer CookieContainer { get; }


        /// <summary>
        /// Build and start a connection to server hub
        /// </summary>
        /// <param name="url"> The url for the hub's connection </param>
        /// <param name="cookies"> A cookie used as a form of authorization </param>
        /// <returns></returns>
        public Task StartHubConnectionAsync();

        /// <summary>
        /// Login using a saved cookie 
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> CookieLoginAsync(string cookie);

        /// <summary>
        /// Login using normal user credentials
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> LoginAsync(string username, SecureString password);

        /// <summary>
        /// Send a registration request to the server
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="confirmationPassword"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> RegsiterAsync(string username, SecureString password, SecureString confirmationPassword);

        /// <summary>
        /// Sends a request to the server that will update the user's project preferences
        /// </summary>
        /// <param name="newProjectPreferences"> The user's new preferences</param>
        /// <returns></returns>
        public Task<HttpResponseMessage> UpdateUserPreferencesAsync(IEnumerable<ProjectType> newProjectPreferences);

    };
}
