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

        #region Public properties

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
        public CookieContainer Cookies { get; }

        /// <summary>
        /// A client handler for cookie storage
        /// </summary>
        public HttpClientHandler ClientHandler { get; set; }

        #endregion


        /// <summary>
        /// Build and start a connection to server hub
        /// </summary>
        /// <param name="url"> The url for the hub's connection </param>
        /// <param name="cookies"> A cookie used as a form of authorization </param>
        /// <returns></returns>
        public Task StartHubConnectionAsync(string url, CookieContainer cookies);


        public Task<HttpResponseMessage> CookieLoginAsync();

        public Task<HttpResponseMessage> LoginAsync(string username, SecureString password);

        public Task<HttpResponseMessage> RegsiterAsync(string username, SecureString password, SecureString confirmationPassword);

        public Task<HttpResponseMessage> UpdateUserPreferencesAsync(IEnumerable<ProjectType> newProjectPreferences);


    };
}
