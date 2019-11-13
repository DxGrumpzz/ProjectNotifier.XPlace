namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    ///
    /// </summary>
    public class AppSettingsViewModel : BaseViewModel
    {

        #region Private fields

        private bool _rememberMe;

        /// <summary>
        /// Application settings
        /// </summary>
        private AppSettingsDataModel _clientAppSettingsModel;

        /// <summary>
        /// A boolean flag that indicates if this is the first time this viewmodel was loaded
        /// </summary>
        private bool _isFirstLoad = true;

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
        /// A boolean flag that indiactes if the user will automatically login next time the app opens
        /// </summary>
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                // Don't user to spam the remember me option,
                // This will be changed later
                if (DI.GetService<ProjectsPageViewModel>().SettingsViewModel.SavedNotificationOpen == true)
                    return;

                if (_rememberMe != value)
                {
                    _rememberMe = value;

                    OnPropertyChanged();

                    if (_isFirstLoad == true)
                    {
                        _isFirstLoad = false;
                        return;
                    }

                    var settings = DI.GetService<IClientDataStore>().GetClientAppSettings();

                    settings.RememberMe = value;
                    // Update config value
                    //_clientAppSettingsModel.RememberMe = value;

                    DI.GetService<IClientDataStore>().SaveClientAppSettings();

                    // Show saved notification
                    Task.Run(ShowSavedNotificationAsync);
                };
            }
        }


        #endregion


        public AppSettingsViewModel()
        {

            _clientAppSettingsModel = DI.ClientAppSettings();


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
                DI.Logger().Log($"User changed {nameof(AppSettingsDataModel.ProjectsToDisplay)} setting to {setting.Value}", LogLevel.Informative);

                // Update value 
                var settings = await DI.GetService<IClientDataStore>().GetClientAppSettingsAsync();

                settings.ProjectsToDisplay = setting.Value;

                await DI.GetService<IClientDataStore>().SaveClientAppSettingsAsync();


                // Load new project list
                DI.GetService<ProjectsPageViewModel>().ProjectList = new ObservableCollection<ProjectItemViewModel>(
                 // Get cached project list
                 DI.GetService<IClientCache>().ProjectListCache
                 // Take however necessary
                 .Take(setting.Value)
                 // Select ProjectModel list to a list of ProjectItemViewModel
                 .Select((project) => new ProjectItemViewModel()
                 {
                     ProjectModel = project,
                 }));


                // Update config value
                //DI.GetService<JsonConfigManager>().WriteSetting(nameof(ClientAppSettingsModel.ProjectsToDisplay), setting.Value);

                // Show settings saved notification
                await ShowSavedNotificationAsync();
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
                DI.Logger().Log($"User changed {nameof(AppSettingsDataModel.KeepNotificationOpenSeconds)} setting to {setting.Value}", LogLevel.Informative);

                // Update value 
                var settings = await DI.GetService<IClientDataStore>().GetClientAppSettingsAsync();

                settings.KeepNotificationOpenSeconds = setting.Value;

                await DI.GetService<IClientDataStore>().SaveClientAppSettingsAsync();

                // Update config value
                //DI.GetService<JsonConfigManager>().WriteSetting(nameof(ClientAppSettingsModel.KeepNotificationOpenSeconds), setting.Value);

                // Show settings saved notification
                await ShowSavedNotificationAsync();
            });
        }


        private async Task ShowSavedNotificationAsync()
        {
            // Show settings saved notification
            await DI.GetService<ProjectsPageViewModel>().SettingsViewModel.ShowSavedNotificationAsync();
        }
    };
};