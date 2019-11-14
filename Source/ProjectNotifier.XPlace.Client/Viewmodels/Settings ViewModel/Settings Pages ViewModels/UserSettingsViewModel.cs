namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    ///
    /// </summary>
    public class UserSettingsViewModel : BaseViewModel
    {

        public static UserSettingsViewModel DesginInstance => new UserSettingsViewModel()
        {

            ProjectPreferences = new ObservableCollection<ProjectTypes>()
            {
                ProjectTypes.Administration,
                ProjectTypes.ArchitectureAndInteriorDesign,
                ProjectTypes.CoachingAndTraining,
                ProjectTypes.Executives,
            },

        };


        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ObservableCollection<ProjectTypes> _projectPreferences;

        #endregion


        #region Public properties

        /// <summary>
        /// The user's project preferences
        /// </summary>
        public ObservableCollection<ProjectTypes> ProjectPreferences
        {
            get => _projectPreferences;
            set
            {
                _projectPreferences = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands

        public ICommand ShowProjectPreferencesMenuCommand { get; }

        #endregion

        public UserSettingsViewModel()
        {
            ShowProjectPreferencesMenuCommand = new RelayCommand(ExecuteShowProjectPreferencesMenuCommand);
        }

        private void ExecuteShowProjectPreferencesMenuCommand()
        {
            


        }
    };
};