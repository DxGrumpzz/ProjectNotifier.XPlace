namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class MainWindowViewModel : BaseViewModel
    {

        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MainWindowModel _model;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isLoading = true;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ProjectListViewModel projectListViewModel;


        #endregion


        #region Public properties

        public MainWindowModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        public ProjectListViewModel ProjectList
        {
            get => projectListViewModel;
            set
            {
                projectListViewModel = value;
                OnPropertyChanged();

            }
        }

        public ClientAppSettingsModel ClientAppSettingsModel { get; }

        public SettingsViewModel SettingsViewModel { get; }

        public ProjectLoader ProjectLoader { get; }





        private BaseView _currentPage;
        public BaseView CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }


        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands

        public RelayCommand OpenSettingsCommand { get; }

        public RelayCommand Command { get; }

        #endregion



        public MainWindowViewModel() { }

        public MainWindowViewModel(ClientAppSettingsModel clientAppSettingsModel, ProjectLoader projectLoader)
        {
            ClientAppSettingsModel = clientAppSettingsModel;
            SettingsViewModel = new SettingsViewModel(clientAppSettingsModel);
            ProjectLoader = projectLoader;


            CurrentPage = new LoginView()
            {
                ViewModel = new LoginViewModel(),
            };


            Task.Run(SetupRSSProjectListAsync);
            ProjectLoader.StartAutoUpdating();


            OpenSettingsCommand = new RelayCommand(() =>
            {
                SettingsViewModel.IsOpen = true;
            });

            // When project count setting is saved...
            SettingsViewModel.ProjectCountSetting.SaveChangesAction += new Action<TextEntryViewModel<int>>(async (value) =>
            {
                // Reset project list
                ProjectList.ProjectList = new ObservableCollection<ProjectItemViewModel>();

                // Display loading text
                IsLoading = true;

                // Load new project list
                await SetupRSSProjectListAsync();
            });


            ProjectLoader.ProjectsListUpdated += (newProjectList) =>
            {
                var currentProjectListFirstProject = ProjectList.ProjectList.First().ProjectModel;
                var newProjectListFirstProject = newProjectList.ProjectList.First().ProjectModel;

                // Comapre the 2 projects 
                if (currentProjectListFirstProject.ProjectID != newProjectListFirstProject.ProjectID)
                {
                    // Find the newer projects
                    var newProjectsList = newProjectList.ProjectList
                    .Where(model =>
                    {
                        return model.ProjectModel.ProjectID > currentProjectListFirstProject.ProjectID;
                    })
                    // Convert results into ProjectModel
                    .Select(result => new ProjectModel()
                    {
                        Title = result.ProjectModel.Title,
                        Link = result.ProjectModel.Link,
                    });


                    // update list
                    ProjectList = new ProjectListViewModel();
                    IsLoading = true;
                    ProjectList = newProjectList;
                    IsLoading = false;


                    // Notify user for the new projects
                    DI.UIManager().ShowProjectNotification(newProjectsList);
                };
            };
        }


        #region Private methods

        /// <summary>
        /// Asynchrounsly loads rss feed content
        /// </summary>
        /// <returns></returns>
        private async Task SetupRSSProjectListAsync()
        {
            // Load projects
            await Task.Run(() => ProjectList = ProjectLoader.LoadProjects());

            // Finished loading content
            IsLoading = false;
        }

        #endregion

    }
}