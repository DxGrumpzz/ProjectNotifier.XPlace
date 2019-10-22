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

        public RelayCommand OpenSettingsCommand { get; }

        #endregion


        public ProjectsPageViewModel()
        {
            SettingsViewModel = new SettingsViewModel(DI.ClientAppSettings());

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

            // Bind hub events
            DI.GetService<IServerConnection>().ProjectsHubConnection.On<IEnumerable<ProjectModel>>("ProjectListUpdated", ProjectListUpdated);
        }




        #region Command Callbacks



        #endregion


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
                .AsEnumerable());

            DI.UIManager().ShowProjectNotification(projects.Take(8));
        }

    };
};
