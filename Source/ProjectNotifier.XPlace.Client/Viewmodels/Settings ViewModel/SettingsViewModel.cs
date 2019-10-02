namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {

        #region Private fields

        private bool _isOpen;

        private bool _isSaved;

        #endregion


        #region Public properties

        /// <summary>
        /// Application settings
        /// </summary>
        public ClientAppSettingsModel ClientAppSettingsModel { get; private set; }

        public TextEntryViewModel<int> ProjectCountSetting { get; private set; }
        public TextEntryViewModel<int> NotificationDispalySecondsSetting { get; private set; }


        public bool ShowSavedNotification
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
            set
            {
                _isOpen = value;
                OnPropertyChanged();
            }
        }


        #endregion


        public RelayCommand CloseSettingsCommand { get; }


        public SettingsViewModel() { }

        public SettingsViewModel(ClientAppSettingsModel settings)
        {
            ClientAppSettingsModel = settings;


            ProjectCountSetting = new TextEntryViewModel<int>(ClientAppSettingsModel.ProjectsToDisplay)
            {
                IsNumericOnly = true,

                MaxLength = 3,


            };
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
                DI.GetService<JsonConfigManager>().WriteSetting(nameof(ClientAppSettingsModel.ProjectsToDisplay), setting.Value);

                // Show settings saved notification
                await ShowSavedNOtificationAsync();
            });



            NotificationDispalySecondsSetting = new TextEntryViewModel<int>(ClientAppSettingsModel.KeepNotificationOpenSeconds)
            {
                IsNumericOnly = true,

                MaxLength = 2,
            };

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
                DI.GetService<JsonConfigManager>().WriteSetting(nameof(ClientAppSettingsModel.KeepNotificationOpenSeconds), setting.Value);

                // Show settings saved notification
                await ShowSavedNOtificationAsync();
            });


            CloseSettingsCommand = new RelayCommand(ExecuteCloseSettingsCommand);
        }


        private void ExecuteCloseSettingsCommand()
        {
            IsOpen = false;
        }



        #region Private helpers

        /// <summary>
        /// Animates the settings saved notification
        /// </summary>
        private async Task ShowSavedNOtificationAsync()
        {
            // Show saved notification
            ShowSavedNotification = true;

            // Wait a-bit
            await Task.Delay(1500);

            // Hide notification
            ShowSavedNotification = false;
        }

        #endregion

    };
};