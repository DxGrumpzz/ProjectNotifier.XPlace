namespace ProjectNotifier.XPlace.Client
{
    using System.Net;
    using System.Net.Http;
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
        public Task StartHubConnection(string url, CookieContainer cookies)
        {
            throw new System.NotImplementedException();
        }
    };
};