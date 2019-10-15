namespace ProjectNotifier.XPlace.WebServer
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// A class that contains a list of loaded projects
    /// </summary>
    public class ProjectList
    {
        
        #region Private properties

        private readonly object _projectsLock = new object();

        private IEnumerable<ProjectModel> _projects;

        private readonly IProjectLoader _projectLoader;

        #endregion

        public IEnumerable<ProjectModel> Projects
        {
            get => _projects;
            private set
            {
                lock (_projectsLock)
                {
                    _projects = value;
                };
            }
        }


        public ProjectList(IProjectLoader projectLoader)
        {
            _projectLoader = projectLoader;
        }

        /// <summary>
        /// Updates the list of projects
        /// </summary>
        public async Task UpdateListAsync()
        {
            Projects = await _projectLoader.LoadProjectsAsync();
        }
    };
};