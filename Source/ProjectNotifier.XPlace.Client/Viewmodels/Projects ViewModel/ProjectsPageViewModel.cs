namespace ProjectNotifier.XPlace.Client
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;

    /// <summary>
    /// 
    /// </summary>
    public class ProjectsPageViewModel : BaseViewModel
    {
        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ObservableCollection<ProjectItemViewModel> _projectList;

        #endregion

        #region Public properties

        public ObservableCollection<ProjectItemViewModel> ProjectList
        {
            get => _projectList;
            set
            {
                _projectList = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands



        #endregion


        public ProjectsPageViewModel()
        {

        }


        #region Command Callbacks



        #endregion

    };
};
