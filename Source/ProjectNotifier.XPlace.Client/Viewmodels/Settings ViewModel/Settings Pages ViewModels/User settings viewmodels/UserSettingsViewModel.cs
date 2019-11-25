namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    ///
    /// </summary>
    public class UserSettingsViewModel : BaseViewModel
    {

        public static UserSettingsViewModel DesginInstance => new UserSettingsViewModel()
        {

            ProjectPreferences = new ObservableCollection<UserProjectPreferenceItemViewModel>()
            {
                new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = ProjectType.Administration,
                },

                new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = ProjectType.ArchitectureAndInteriorDesign,
                },

                new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = ProjectType.CoachingAndTraining,
                },

                new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = ProjectType.Executives,
                },
            },

        };


        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ObservableCollection<UserProjectPreferenceItemViewModel> _projectPreferences;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _hasPreferences;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _saveChangesEnbaled = true;

        #endregion


        #region Public properties

        /// <summary>
        /// The user's project preferences
        /// </summary>
        public ObservableCollection<UserProjectPreferenceItemViewModel> ProjectPreferences
        {
            get => _projectPreferences;
            set
            {
                _projectPreferences = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Menu for selecting project preferences
        /// </summary>
        public ProjectPreferenceSelectionMenuViewModel ProjectPreferenceSelectionMenuViewModel { get; set; }


        /// <summary>
        /// A boolean flag that indicates if the user has selected preferences
        /// </summary>
        public bool HasPreferences
        {
            get => _hasPreferences;
            set
            {
                _hasPreferences = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A boolean flag that indicates if the save changes button is enabled
        /// </summary>
        public bool SaveChangesEnbaled
        {
            get => _saveChangesEnbaled;
            set
            {
                _saveChangesEnbaled = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands

        public ICommand ShowProjectPreferencesMenuCommand { get; }

        public ICommand SaveChangesCommand { get; }

        #endregion


        public UserSettingsViewModel()
        {
            var userProfile = DI.GetService<IClientDataStore>().GetUserProfile();


            if (userProfile.UserProjectPreferences != null)
            {
                // Setup project preferences list by...
                ProjectPreferences = new ObservableCollection<UserProjectPreferenceItemViewModel>(
                    // Getting the user's project preferences
                    userProfile.UserProjectPreferences
                    // And converting the projects to UserProjectPreferenceItemViewModel
                    .Select(projectType => new UserProjectPreferenceItemViewModel()
                    {
                        ProjectType = projectType,
                    }));

                // Project preferences present
                if (ProjectPreferences.Count > 0)
                    // Update flag
                    HasPreferences = true;

                // Setup ProjectPreferenceSelectionMenuViewModel by...
                ProjectPreferenceSelectionMenuViewModel = new ProjectPreferenceSelectionMenuViewModel(
                    // Getting the user's project preferences
                    userProfile.UserProjectPreferences);
            }
            else
            {
                // Setup ProjectPreferenceSelectionMenuViewModel with an empty project types list
                ProjectPreferenceSelectionMenuViewModel = new ProjectPreferenceSelectionMenuViewModel(Enumerable.Empty<ProjectType>());
            };


            ShowProjectPreferencesMenuCommand = new RelayCommand(ExecuteShowProjectPreferencesMenuCommand);
            SaveChangesCommand = new RelayCommand(ExecuteSaveChangesCommand);
        }


        private async Task ExecuteSaveChangesCommand()
        {
            await RunCommandAsync(() => SaveChangesEnbaled,
            async () =>
            {
                // The user's profile
                var userProfile = DI.GetService<IClientDataStore>().GetUserProfile();

                // Get the user's preferences as an enumerable of ProjectType
                var newPreferences = ProjectPreferences.Select(projectType => projectType.ProjectType);

                // Update user's profile for project preference changes
                var updateProfileRequest = await DI.GetService<IServerConnection>().Client
                    .PostAsJsonAsync("Https://localhost:5001/Profile/UpdateUserPreferences/{ProjectType}", newPreferences);

                // If request was succesfull
                if (updateProfileRequest.IsSuccessStatusCode == true)
                {
                    // Update user profile
                    userProfile.UserProjectPreferences = newPreferences;

                    // Update projects page
                    DI.GetService<ProjectsPageViewModel>().UpdateProjectsList(userProfile.UserProjectPreferences);


                    // log new profile update
                    DI.Logger().Log(
                       logMessage: $"User updated profile", 
                       LogLevel.Informative);
                }
                // If request failed
                else
                {
                    // Display in logger the error
                    DI.Logger().Log(
                        logMessage: $"Something happend while updating preferences \n[Server returned {(int)updateProfileRequest.StatusCode}/{updateProfileRequest.StatusCode}]",
                        logLevel: LogLevel.Critical);
                };
            },
            invertFlag: true);
        }

        private void ExecuteShowProjectPreferencesMenuCommand()
        {
            ProjectPreferenceSelectionMenuViewModel.IsMenuOpen = true;
        }
    };
};