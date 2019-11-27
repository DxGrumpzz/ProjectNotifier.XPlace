namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

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
        /// A list that returns the user's preferred projects
        /// </summary>
        public IEnumerable<ProjectModel> UserPrefferedProjectsCache
        {
            get
            {
                // Get user's profile
                var userProfile = DI.GetService<IClientDataStore>().GetUserProfile();

                // Get a project list that only the user's preffered project types
                var projects = ProjectListCache
                .Where(project =>
                {
                    // Get the user's project preferences
                    var projects = userProfile.UserProjectPreferences
                    .Where(preference =>
                    {
                        // Check if the current project has at least one of the user's project preferences
                        if (project.ProjectTypes.Contains(preference) == true)
                            return true;
                        else
                            return false;
                    });

                    // Check if projets has a value meaning that the current project is relevant to the user
                    if ((projects != null) && (projects.Count() > 0))
                        return true;
                    else
                        return false;
                });

                return projects;
            }
        }

    };
};
