namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        public static SettingsViewModel DesignInstance_ProjectsPageViewModel => new SettingsViewModel()
        {
            IsOpen = false,
            CurrentSettingsPage = new BaseView(),
        };


        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isOpen;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isSaved;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _isOpening;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BaseView _currentSettingsPage;
        
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _goBackButtonVisible;

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
                // Update GoBackButtonVisible button if display a settings page
                if (!(value is SettingsListView))
                    GoBackButtonVisible = true;
                else
                    GoBackButtonVisible = false;


                _currentSettingsPage = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// A boolean flag that indicates if the return the settings list button should be visible
        /// </summary>
        public bool GoBackButtonVisible
        {
            get => _goBackButtonVisible;
            set 
            { 
                _goBackButtonVisible = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands

        public RelayCommand CloseSettingsCommand { get; }

        public RelayCommand GoBackCommand { get; }

        #endregion


        public SettingsViewModel()
        {
            CurrentSettingsPage = new SettingsListView()
            {
                ViewModel = new SettingsListViewModel()
            };


            CloseSettingsCommand = new RelayCommand(ExecuteCloseSettingsCommand);

            GoBackCommand = new RelayCommand(ExecuteGoBackCommand);
        }

        

        #region Commnad callbacks

        private void ExecuteCloseSettingsCommand()
        {
            if (_isOpening == true)
                return;

            IsOpen = false;
        }

        private void ExecuteGoBackCommand()
        {
            CurrentSettingsPage = new SettingsListView()
            {
                ViewModel = new SettingsListViewModel(),
            };
        }

        #endregion


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