namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    /// A viewmodel class for a project preference type 
    /// </summary>
    public class ProjectPreferenceMenuItemViewModel : BaseViewModel
    {

        #region Public properties

        /// <summary>
        /// The project types that the user can select 
        /// </summary>
        public ProjectType ProjectType { get; set; }

        #endregion


        #region Commands

        public ICommand AddProjectTypeCommand { get; }

        #endregion


        public ProjectPreferenceMenuItemViewModel()
        {
            AddProjectTypeCommand = new RelayCommand(ExecuteAddProjectTypeCommand);
        }


        #region Command callbacks

        private void ExecuteAddProjectTypeCommand()
        {
            // TODO: improve later 


            // Get user settings viewmodel
            var vm = ((UserSettingsViewModel)DI.GetService<ProjectsPageViewModel>().SettingsViewModel.CurrentSettingsPage.ViewModel);

            // Add this item to user preffered project list
            vm.ProjectPreferences.Add(new UserProjectPreferenceItemViewModel()
            {
                ProjectType = ProjectType,
            });

            // Sort list alphabetically
            vm.ProjectPreferences = new ObservableCollection<UserProjectPreferenceItemViewModel>(
            vm.ProjectPreferences
            .OrderBy(project => project.ProjectType.ToString()));


            // Remove this item from project preference selection menu
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectType.Remove(this);


            // Sort list alphabetically
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectType = new ObservableCollection<ProjectPreferenceMenuItemViewModel>(
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectType
            .OrderBy(project => project.ProjectType.ToString()));


            // Update HasPreferences flag
            vm.HasPreferences = true;
            // Update save changes button
            vm.SaveChangesEnbaled = true;

            // If user added last project type
            if (vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectType.Count == 0)
            {
                // Close project preferce menu
                vm.ProjectPreferenceSelectionMenuViewModel.IsMenuOpen = false;
            };
        }

        #endregion

    };
};
