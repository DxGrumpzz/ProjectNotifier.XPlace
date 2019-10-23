namespace ProjectNotifier.XPlace.WebServer
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;

    /// <summary>
    /// 
    /// </summary>
    [Authorize()]
    public class ProjectsHub : Hub
    {

        public ProjectsHub()
        {
            
        }
    };
};