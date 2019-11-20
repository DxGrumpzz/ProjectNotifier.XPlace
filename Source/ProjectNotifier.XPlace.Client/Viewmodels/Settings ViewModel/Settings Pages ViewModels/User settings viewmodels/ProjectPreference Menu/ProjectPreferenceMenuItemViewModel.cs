namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.Generic;
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
        public ProjectTypes ProjectType { get; set; }

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

            var userProfile = DI.GetService<IClientDataStore>().GetUserProfile();

            // Get user settings viewmodel
            var vm = ((UserSettingsViewModel)DI.GetService<ProjectsPageViewModel>().SettingsViewModel.CurrentSettingsPage.ViewModel);

            // Add this item to user preffered project list
            vm.ProjectPreferences.Add(new UserProjectPreferenceItemViewModel()
            {
                ProjectType = ProjectType,
            });

            // Remove this item from project preference selection menu
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectTypes.Remove(this);

            // Update UserProfile project list
            userProfile.UserProjectPreferences = new List<UserProjectPreference>(
                vm.ProjectPreferences
                .Select(projectType => new UserProjectPreference()
                {
                    ProjectType = projectType.ProjectType,
                    User = userProfile,
                }));

            // Update HasPreferences flag
            vm.HasPreferences = true;

            // If user added last project type
            if (vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectTypes.Count == 0)
            {
                // Close project preferce menu
                vm.ProjectPreferenceSelectionMenuViewModel.IsMenuOpen = false;
            };
        } 

        #endregion

    };
};
