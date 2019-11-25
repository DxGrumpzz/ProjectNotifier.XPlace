namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;

    /// <summary>
    /// An implementation of a basic client side caching "system"
    /// </summary>
    public class ClientCache : IClientCache
    {
      
        /// <summary>
        /// A list that holds the latest projects available
        /// </summary>
        public IEnumerable<ProjectModel> ProjectListCache { get; set; }

        /// <summary>
        /// A list that holds the user's preffered projects
        /// </summary>
        public IEnumerable<ProjectModel> UserPrefferedProjectsCache { get; set; }

    };
};
