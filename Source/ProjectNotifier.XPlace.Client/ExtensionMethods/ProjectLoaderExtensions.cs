namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A helper class for <see cref="ProjectLoader"/> that provides extension methods
    /// </summary>
    public static class ProjectLoaderExtensions
    {

        /// <summary>
        /// Loads a list of Projects and converts them to an <see cref="ObservableCollection<ProjectItemViewModel> "/>
        /// </summary>
        /// <param name="projectLoader"></param>
        /// <returns></returns>
        public async static Task<ObservableCollection<ProjectItemViewModel>> LoadProjectsAsObservableAsync(this IProjectLoader projectLoader, CancellationToken cancellationToken = default)
        {
            // Get a list of projects
            var projects = (await projectLoader.LoadProjectsAsync(cancellationToken))
            // Convert the ProjectModel to a ProjectItemViewModel
            .Select(project => new ProjectItemViewModel()
            {
                ProjectModel = project,
            });

            // Return the projects as an ObservableCollection
            return new ObservableCollection<ProjectItemViewModel>(projects);
        }

    };
};
