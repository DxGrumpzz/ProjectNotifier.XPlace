namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System.Collections.ObjectModel;
    using System.Diagnostics;

    /// <summary>
    ///
    /// </summary>
    public class UserSettingsViewModel : BaseViewModel
    {

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


        public UserSettingsViewModel()
        {

        }



    };
};