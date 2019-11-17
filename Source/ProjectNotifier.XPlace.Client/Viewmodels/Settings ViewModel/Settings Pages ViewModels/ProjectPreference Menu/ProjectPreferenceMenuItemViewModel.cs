namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

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

        } 

        #endregion

    };
};
