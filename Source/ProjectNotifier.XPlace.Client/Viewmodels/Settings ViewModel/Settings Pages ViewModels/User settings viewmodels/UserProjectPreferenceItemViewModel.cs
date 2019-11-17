namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
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
            RemovePreferenceCommand = new RelayCommand(ExecuteRemovePreference);
        }


        private void ExecuteRemovePreference()
        {
            
        }
    };
};
