namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///
    /// </summary>
    public class SettingsListViewModel : BaseViewModel
    {

        public List<SettingIconViewModel> SettingIcons { get; set; }


        public SettingsListViewModel()
        {

            SettingIcons = new List<SettingIconViewModel>()
            {
                new SettingIconViewModel()
                {
                    Description = "הגדרות אפליקצייה",
                    Icon = SettingIcon.ApplicationSettings,

                    GotoSettingCommand = new RelayCommand(() =>
                    {
                        var s = DI.GetService<ProjectsPageViewModel>().SettingsViewModel.CurrentSettingsPage = new AppSettingsView()
                        {
                            ViewModel = new AppSettingsViewModel(),
                        };
                    }),
                },

                new SettingIconViewModel()
                {
                    Description = "הגדרות משתמש",
                    Icon = SettingIcon.UserSettings,

                    GotoSettingCommand = new RelayCommand(() =>
                    {
                        // Get user profile
                        var userProfile = DI.GetService<IClientDataStore>().GetUserProfile();

                        // Temporary list
                        var userProjectPreferences = new ObservableCollection<UserProjectPreferenceItemViewModel>();

                        // If user has project preferences
                        if(userProfile.UserProjectPreferences != null)
                        {
                            // Create an observable list of UserProjectPreferenceItemViewModel by...
                            userProjectPreferences = new ObservableCollection<UserProjectPreferenceItemViewModel>(
                                // Getting the  user's project preferences
                                userProfile.UserProjectPreferences
                                // Cast the list of ProjectType to UserProjectPreferenceItemViewModel
                                .Select(projectType => 
                                new UserProjectPreferenceItemViewModel()
                                {
                                    ProjectType = projectType,
                                }));
                        };

                        // Switch to User settings page
                        DI.GetService<ProjectsPageViewModel>().SettingsViewModel.CurrentSettingsPage = new UserSettingsView()
                        {
                            ViewModel = new UserSettingsViewModel()
                            {
                                ProjectPreferences = userProjectPreferences,
                            },
                        };
                    }),
                },
            };
        }
    };
};
