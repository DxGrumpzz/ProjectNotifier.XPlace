namespace ProjectNotifier.XPlace.Core
{
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

    };
}
