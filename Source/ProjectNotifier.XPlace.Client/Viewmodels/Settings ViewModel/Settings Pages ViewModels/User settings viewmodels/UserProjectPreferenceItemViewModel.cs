﻿namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Input;


    /// <summary>
    /// A viewmodel for a user's alredy preffered project type 
    /// </summary>
    public class UserProjectPreferenceItemViewModel : BaseViewModel
    {

        #region Public properties

        public ProjectType ProjectType { get; set; }

        #endregion


        #region Commands

        public ICommand RemovePreferenceCommand { get; }

        #endregion


        public UserProjectPreferenceItemViewModel()
        {
            RemovePreferenceCommand = new RelayCommand(ExecuteRemovePreferenceCommand);
        }


        private void ExecuteRemovePreferenceCommand()
        {
            // TODO: improve later 

            // Get user settings viewmodel
            var vm = ((UserSettingsViewModel)DI.GetService<ProjectsPageViewModel>().SettingsViewModel.CurrentSettingsPage.ViewModel);

            // Add this item to user preffered project list
            vm.ProjectPreferences.Remove(this);

            // Sort list alphabetically
            vm.ProjectPreferences = new ObservableCollection<UserProjectPreferenceItemViewModel>(
            vm.ProjectPreferences
            .OrderBy(project => project.ProjectType.ToHebrewString(), StringComparer.Create(new CultureInfo("he-IL"), true)));

            // Remove this item from project preference selection menu
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectType.Add(new ProjectPreferenceMenuItemViewModel()
            {
                ProjectType = ProjectType,
            });

            // Sort list alphabetically
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectType = new ObservableCollection<ProjectPreferenceMenuItemViewModel>(
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectType
            .OrderBy(project => project.ProjectType.ToHebrewString(), StringComparer.Create(new CultureInfo("he-IL"), true)));
            

            // If user has removed every preference
            if (vm.ProjectPreferences.Count == 0)
            {
                // Update HasPreferences flag
                vm.HasPreferences = false;
                // Update save changes button flag
                vm.SaveChangesEnbaled = false;
            };
        }
    };
};
