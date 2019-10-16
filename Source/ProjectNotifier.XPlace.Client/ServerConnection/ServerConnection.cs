namespace ProjectNotifier.XPlace.Client
{
    using Microsoft.AspNetCore.SignalR.Client;
    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// An implementation of <see cref="IServerConnection"/>
    /// </summary>
    public class ServerConnection : IServerConnection
    {
        /// <summary>
        /// The client's Projects hub connection
        /// </summary>
        public HubConnection ProjectsHubConnection { get; set; }

    }
}