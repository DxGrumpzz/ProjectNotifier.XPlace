namespace ProjectNotifier.XPlace.Client
{
    using Microsoft.AspNetCore.SignalR.Client;

    using ProjectNotifier.XPlace.Core;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// 
    /// </summary>
    public class ProjectsPageViewModel : BaseViewModel
    {

        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ObservableCollection<ProjectItemViewModel> _projectList;

        /// <summary>
        /// Holds the max height of the viewport
        /// </summary>
        private double _viewportHeight;

        #endregion


        #region Public properties

        public ObservableCollection<ProjectItemViewModel> ProjectList
        {
            get => _projectList;
            set
            {
                _projectList = value;
                OnPropertyChanged();
            }
        }


        public SettingsViewModel SettingsViewModel { get; set; }

        #endregion


        #region Commands

        public ICommand OpenSettingsCommand { get; }
        public ICommand ProjectListScrolledCommand { get; }

        #endregion


        public ProjectsPageViewModel()
        {
            SettingsViewModel = new SettingsViewModel();


            // Open settings page when user clicks the settings button
            OpenSettingsCommand = new RelayCommand(SettingsViewModel.OpenSettings);

            ProjectListScrolledCommand = new RelayCommand<ScrollChangedEventArgs>(ExecuteProjectListScrolled);


            // Bind hub events
            DI.GetService<IServerConnection>().ProjectsHubConnection.On<IEnumerable<ProjectModel>>("ProjectListUpdated", ProjectListUpdated);
        }


        #region Command Callbacks


        /// <summary>
        /// A command that will be executed when the project list scrollviewer is scrolled
        /// </summary>
        /// <param name="eventArg"></param>
        private void ExecuteProjectListScrolled(ScrollChangedEventArgs eventArg)
        {
            // Get the event source/sender (the scrollviwer object)
            ScrollViewer sender = eventArg.Source as ScrollViewer;

            // Update max viewport height
            UpdateViewportHeight(sender.ScrollableHeight);

            // Check if loading the projects is even neccessary 
            if (ProjectList.Count < DI.ClientAppSettings().ProjectsToDisplay)
            {
                // Check if the vertical position of the scrollbar is low enough
                if (sender.VerticalOffset >= (_viewportHeight - 10))
                {
                    // The number of projects avaiable to load
                    int projectsCount = DI.ClientAppSettings().ProjectsToDisplay - ProjectList.Count;

                    // How big of a chuck to take out of the cached project list
                    int chunccSize = 5 > projectsCount ?
                        projectsCount :
                        5;

                    // Get a chunk out of the cached projectlist
                    DI.GetService<IClientCache>().ProjectListCache
                    // Convert it to a List<> to get more functionality
                    .ToList()
                    // Get a "Chuck" that contians the missing projects
                    .GetRange(ProjectList.Count, chunccSize)
                    // For every project
                    .ForEach(project =>
                    {
                        // Add it to the project list
                        ProjectList.Add(new ProjectItemViewModel()
                        {
                            ProjectModel = project,
                        });
                    });
                };
            };
        }

        #endregion



        /// <summary>
        /// Updates/refreshes the displayed project list depending on the passed <paramref name="projectTypes"/> 
        /// </summary>
        /// <param name="projectTypes"></param>
        public void UpdateProjectsList(IEnumerable<ProjectType> projectTypes)
        {
            ProjectList = new ObservableCollection<ProjectItemViewModel>(
            DI.GetService<IClientCache>().ProjectListCache
            // Match projects where the user's project preference matches the project's
            .Where(project =>
            {
                foreach (var userProject in projectTypes)
                {
                    if (project.ProjectTypes.Contains(userProject))
                    {
                        return true;
                    };
                };

                return false;
            })
            // Convert the ProjectModels to a ProjectItemViewModel
            .Select((p) => 
            new ProjectItemViewModel()
            {
                ProjectModel = p,
            })
            // Take the first 10
            .Take(10));
        }


        #region Private helpers

        private void UpdateViewportHeight(double newviewportHeight)
        {
            // Set viewport height if it is bigger
            if (newviewportHeight > _viewportHeight)
                _viewportHeight = newviewportHeight;
        }


        private void ProjectListUpdated(IEnumerable<ProjectModel> projects)
        {
            // Update project list cache 
            DI.GetService<IClientCache>().ProjectListCache = projects;


            // Reset project list
            ProjectList = new ObservableCollection<ProjectItemViewModel>();

            // Display loading text
            //IsLoading = true

            // Load new project list
            ProjectList = new ObservableCollection<ProjectItemViewModel>(
                projects
                .Select((project) => new ProjectItemViewModel()
                {
                    ProjectModel = project,
                })
                .Take(DI.ClientAppSettings().ProjectsToDisplay)
                .AsEnumerable());

            DI.UIManager().ShowProjectNotification(projects.Take(8));
        }

        #endregion
    };
};