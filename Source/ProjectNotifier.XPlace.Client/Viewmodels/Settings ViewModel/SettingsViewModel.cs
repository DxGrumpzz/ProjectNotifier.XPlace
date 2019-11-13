namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {

        #region Private fields

        private bool _isOpen;

        private bool _isSaved;

        private bool _isOpening;

        private BaseView _currentSettingsPage;

        #endregion


        #region Public properties

        /// <summary>
        /// A boolean flag that indicates if the settings saved notification is open
        /// </summary>
        public bool SavedNotificationOpen
        {
            get => _isSaved;
            private set
            {
                _isSaved = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// A boolean flag that indicates if this control is open or in view
        /// </summary>
        public bool IsOpen
        {
            get => _isOpen;
            private set
            {
                _isOpen = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Currently displayed settings page view
        /// </summary>
        public BaseView CurrentSettingsPage
        {
            get => _currentSettingsPage;
            set
            {
                _currentSettingsPage = value;
                OnPropertyChanged();
            }
        }

        #endregion


        public RelayCommand CloseSettingsCommand { get; }


        public SettingsViewModel()
        {
            CurrentSettingsPage = new SettingsListView()
            {
                ViewModel = new SettingsListViewModel()
            };

            CloseSettingsCommand = new RelayCommand(ExecuteCloseSettingsCommand);
        }


        #region Public methods

        public async Task OpenSettings()
        {
            // Reset settings view to default settigns selection view
            CurrentSettingsPage = new SettingsListView()
            {
                DataContext = new SettingsListViewModel(),
            };

            IsOpen = true;
            _isOpening = true;

            // Wait for opening slide animtaion to finish
            await Task.Delay(600);

            // Allow user to close
            _isOpening = false;
        }

        #endregion


        #region Commnad callbacks

        private void ExecuteCloseSettingsCommand()
        {
            if (_isOpening == true)
                return;

            IsOpen = false;
        }

        #endregion


        #region Private helpers

        /// <summary>
        /// Animates the settings saved notification
        /// </summary>
        public async Task ShowSavedNotificationAsync()
        {
            if (SavedNotificationOpen == true)
                return;

            // Show saved notification
            SavedNotificationOpen = true;

            // Wait a-bit
            await Task.Delay(1500);

            // Hide notification
            SavedNotificationOpen = false;
        }

        #endregion

    };
};