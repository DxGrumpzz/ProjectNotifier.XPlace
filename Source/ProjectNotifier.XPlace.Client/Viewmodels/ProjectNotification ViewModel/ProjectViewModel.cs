namespace ProjectNotifier.XPlace.Client
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// 
    /// </summary>
    public class ProjectViewModel : BaseViewModel
    {

        private ObservableCollection<ProjectItemViewModel> _projectList;

        public ObservableCollection<ProjectItemViewModel> ProjectList
        {
            get => _projectList;
            set
            {
                _projectList = value;
                OnPropertyChanged();
            }
        }


    };
};
