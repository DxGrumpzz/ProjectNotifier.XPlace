namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
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

        private bool _rememberMe;

        /// <summary>
        /// Application settings
        /// </summary>
        private ClientAppSettingsModel _clientAppSettingsModel;

        #endregion


        #region Public properties



        /// <summary>
        /// How many projects are currently being disaplyed
        /// </summary>
        public TextEntryViewModel<int> ProjectCountSetting { get; private set; }

        /// <summary>
        /// How long the new projects notification will be displayed for
        /// </summary>
        public TextEntryViewModel<int> NotificationDispalySecondsSetting { get; private set; }


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
        /// A boolean flag that indiactes if the user will automatically login next time the app opens
        /// </summary>
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                OnPropertyChanged();

                // Update config value
                _clientAppSettingsModel.RememberMe = value;

                // Show saved notification
                Task.Run(ShowSavedNOtificationAsync);
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


        #endregion


        public RelayCommand CloseSettingsCommand { get; }


        public SettingsViewModel()
        {
            _clientAppSettingsModel = DI.GetService< ClientAppSettingsModel>();


            ProjectCountSetting = new TextEntryViewModel<int>(_clientAppSettingsModel.ProjectsToDisplay)
            {
                IsNumericOnly = true,

                MaxLength = 3,
            };

            NotificationDispalySecondsSetting = new TextEntryViewModel<int>(_clientAppSettingsModel.KeepNotificationOpenSeconds)
            {
                IsNumericOnly = true,

                MaxLength = 2,
            };

            RememberMe = _clientAppSettingsModel.RememberMe;


            // Bind events
            ProjectCountSetting.ValueValidationAction += new Func<TextEntryViewModel<int>, bool>(setting =>
            {
                // If value didn't change don't update
                if (setting.Value == setting.PreviousValue)
                {
                    return false;
                };

                // Boundry validation
                if (setting.Value > 100)
                {
                    setting.Value = 100;
                    return false;
                }
                else if (setting.Value < 25)
                {
                    setting.Value = 25;
                    return false;
                };

                return true;
            });

            ProjectCountSetting.SaveChangesAction += new Action<TextEntryViewModel<int>>(async (setting) =>
            {
                DI.Logger().Log($"User changed {nameof(ClientAppSettingsModel.ProjectsToDisplay)} setting to {setting.Value}", LogLevel.Informative);

                // Update value 
                DI.ClientAppSettings().ProjectsToDisplay = setting.Value;

                // Update config value
                //DI.GetService<JsonConfigManager>().WriteSetting(nameof(ClientAppSettingsModel.ProjectsToDisplay), setting.Value);

                // Show settings saved notification
                await ShowSavedNOtificationAsync();
            });

            NotificationDispalySecondsSetting.ValueValidationAction += new Func<TextEntryViewModel<int>, bool>(setting =>
            {
                // If value didn't change don't update
                if (setting.Value == setting.PreviousValue)
                {
                    return false;
                };

                // Boundry validation
                if (setting.Value > 10)
                {
                    setting.Value = 10;
                    return false;
                }
                else if (setting.Value < 3)
                {
                    setting.Value = 3;
                    return false;
                };

                return true;
            });

            NotificationDispalySecondsSetting.SaveChangesAction += new Action<TextEntryViewModel<int>>(async (setting) =>
            {
                DI.Logger().Log($"User changed {nameof(ClientAppSettingsModel.KeepNotificationOpenSeconds)} setting to {setting.Value}", LogLevel.Informative);

                // Update value 
                DI.ClientAppSettings().KeepNotificationOpenSeconds = setting.Value;

                // Update config value
                //DI.GetService<JsonConfigManager>().WriteSetting(nameof(ClientAppSettingsModel.KeepNotificationOpenSeconds), setting.Value);

                // Show settings saved notification
                await ShowSavedNOtificationAsync();
            });


            CloseSettingsCommand = new RelayCommand(ExecuteCloseSettingsCommand);
        }


        #region Public methods

        public async Task OpenSettings()
        {
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
        private async Task ShowSavedNOtificationAsync()
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