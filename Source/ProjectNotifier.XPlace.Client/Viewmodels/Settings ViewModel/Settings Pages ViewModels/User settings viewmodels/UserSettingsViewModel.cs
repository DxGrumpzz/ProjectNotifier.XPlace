namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
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
                    ProjectType = ProjectTypes.Administration,
                },

                new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = ProjectTypes.ArchitectureAndInteriorDesign,
                },

                new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = ProjectTypes.CoachingAndTraining,
                },

                new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = ProjectTypes.Executives,
                },
            },

        };


        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ObservableCollection<UserProjectPreferenceItemViewModel> _projectPreferences;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _hasPreferences;

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

        #endregion


        #region Commands

        public ICommand ShowProjectPreferencesMenuCommand { get; }

        #endregion


        public UserSettingsViewModel()
        {
            var userProfile = DI.GetService<IClientDataStore>().GetUserProfile();


            // Setup project preferences list by...
            ProjectPreferences = new ObservableCollection<UserProjectPreferenceItemViewModel>(
                // Getting the user's project preferences
                userProfile.UserProjectPreferences
                // And converting the projects to UserProjectPreferenceItemViewModel
                .Select(projectType => new UserProjectPreferenceItemViewModel()
                {
                    ProjectType = projectType.ProjectType,
                }));


            // Setup ProjectPreferenceSelectionMenuViewModel by...
            ProjectPreferenceSelectionMenuViewModel = new ProjectPreferenceSelectionMenuViewModel(
                // Getting the user's project preferences
                userProfile.UserProjectPreferences
                // And converting the projects to ProjectTypes
                .Select(projectType => projectType.ProjectType));


            ShowProjectPreferencesMenuCommand = new RelayCommand(ExecuteShowProjectPreferencesMenuCommand);
        }

        private void ExecuteShowProjectPreferencesMenuCommand()
        {
            ProjectPreferenceSelectionMenuViewModel.IsMenuOpen = true;
        }
    };
};