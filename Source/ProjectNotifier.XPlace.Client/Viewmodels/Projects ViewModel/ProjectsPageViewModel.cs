namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.SignalR.Client;
    using System.Net.Http;
    using System;
    using System.Windows.Input;
    using System.Windows.Controls;

    /// <summary>
    /// 
    /// </summary>
    public class ProjectsPageViewModel : BaseViewModel
    {
        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ObservableCollection<ProjectItemViewModel> _projectList;

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

            // Bind settings view events
            SettingsViewModel.ProjectCountSetting.SaveChangesAction += (value) =>
            {
                // Request new project list
                ProjectList = new ObservableCollection<ProjectItemViewModel>();

                // Display loading text
                //IsLoading = true

                // Load new project list
                ProjectList = new ObservableCollection<ProjectItemViewModel>(
                 // Get cached project list
                 DI.GetService<IClientCache>().ProjectListCache
                 // Take however necessary
                 .Take(value.Value)
                 // Select ProjectModel list to a list of ProjectItemViewModel
                 .Select((project) => new ProjectItemViewModel()
                 {
                     ProjectModel = project,
                 }));
            };


            // Open settings page when user clicks the settings button
            OpenSettingsCommand = new RelayCommand(SettingsViewModel.OpenSettings);

            ProjectListScrolledCommand = new RelayCommand<ScrollChangedEventArgs>(ExecuteProjectListScrolled);


            // Bind hub events
            DI.GetService<IServerConnection>().ProjectsHubConnection.On<IEnumerable<ProjectModel>>("ProjectListUpdated", ProjectListUpdated);
        }




        #region Command Callbacks

        private double _viewportHeight;

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
                    int projectsCount = DI.GetService<IClientCache>().ProjectListCache.Count() - ProjectList.Count;

                    // How big of a chuck to take out of the cached project list
                    int chunccSize = 5 > projectsCount ? 
                        projectsCount : 
                        5;

                    // Get a chunk out of the cached projectlist
                    var projects = DI.GetService<IClientCache>().ProjectListCache
                        .ToList()
                        .GetRange(ProjectList.Count, chunccSize)
                        .AsEnumerable();

                    // Add the projects to this project list
                    foreach (var project in projects)
                    {
                        ProjectList.Add(new ProjectItemViewModel()
                        {
                            ProjectModel = project,
                        });
                    };

                    // Log project list update
                    DI.Logger().Log($"Project list updated, \nDisplaying {ProjectList.Count} projects", LogLevel.Informative);
                };
            };
        }

        #endregion


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
