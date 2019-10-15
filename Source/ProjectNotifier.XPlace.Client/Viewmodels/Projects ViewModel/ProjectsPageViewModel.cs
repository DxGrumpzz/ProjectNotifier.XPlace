namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;

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
            SettingsViewModel.ProjectCountSetting.SaveChangesAction += async (value) =>
            {
                // Request new project list
                var response = await DI.GetService<HttpClient>().GetAsync($"Https://LocalHost:5001/Projects/{value.Value}");

                // If an error occured
                if (response.IsSuccessStatusCode == false)
                    return;
                else
                {
                    // Reset project list
                    ProjectList = new ObservableCollection<ProjectItemViewModel>();

                    // Display loading text
                    //IsLoading = true

                    // Load new project list
                    ProjectList = new ObservableCollection<ProjectItemViewModel>(
                        (await response.Content.ReadAsAsync<IEnumerable<ProjectModel>>())
                        .Select((project) => new ProjectItemViewModel()
                        {
                            ProjectModel = project,
                        })
                        .AsEnumerable());
                }
            };



            // Open settings page when user clicks the settings button
            OpenSettingsCommand = new RelayCommand(SettingsViewModel.OpenSettings);
        }


        #region Command Callbacks



        #endregion

    };
};
