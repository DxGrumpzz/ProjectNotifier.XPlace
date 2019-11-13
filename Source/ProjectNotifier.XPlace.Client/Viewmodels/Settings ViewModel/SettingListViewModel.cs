namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

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
                        var s = DI.GetService<ProjectsPageViewModel>().SettingsViewModel.CurrentSettingsPage = new UserSettingsView();
                    }),
                },
            };
        }
    };
};
