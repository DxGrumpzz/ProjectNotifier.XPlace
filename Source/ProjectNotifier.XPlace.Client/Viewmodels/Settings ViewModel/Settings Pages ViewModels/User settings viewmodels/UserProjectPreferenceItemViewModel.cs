namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Linq;
    using System.Windows.Input;


    /// <summary>
    /// A viewmodel for a user's alredy preffered project type 
    /// </summary>
    public class UserProjectPreferenceItemViewModel : BaseViewModel
    {

        #region Public properties

        public ProjectTypes ProjectType { get; set; }

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

            var userProfile = DI.GetService<IClientDataStore>().GetUserProfile();

            // Get user settings viewmodel
            var vm = ((UserSettingsViewModel)DI.GetService<ProjectsPageViewModel>().SettingsViewModel.CurrentSettingsPage.ViewModel);

            // Add this item to user preffered project list
            vm.ProjectPreferences.Remove(this);

            // Remove this item from project preference selection menu
            vm.ProjectPreferenceSelectionMenuViewModel.AvailableProjectTypes.Add(new ProjectPreferenceMenuItemViewModel()
            {
                ProjectType = ProjectType,
            });


            // Update UserProfile project list
            userProfile.UserProjectPreferences = vm.ProjectPreferences
                .Select(projectType => new UserProjectPreference()
                {
                    ProjectType = projectType.ProjectType,
                    User = userProfile,
                })
                .AsEnumerable();


            // If user added last project type
            //if (vm.ProjectPreferences.Count == 0)
            //{

            //};
        }
    };
};
