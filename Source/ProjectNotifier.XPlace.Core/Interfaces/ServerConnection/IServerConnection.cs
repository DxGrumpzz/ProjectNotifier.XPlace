namespace ProjectNotifier.XPlace.Core
{
    using System.Net;
    using System.Net.Http;
    using Microsoft.AspNetCore.SignalR.Client;

    /// <summary>
    /// An interface that holds a "list" of hub connection available to the client
    /// </summary>
    public interface IServerConnection
    {
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
    };
}
