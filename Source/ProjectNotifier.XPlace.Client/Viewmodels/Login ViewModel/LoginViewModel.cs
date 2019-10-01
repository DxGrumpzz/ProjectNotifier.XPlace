namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Collections.ObjectModel;
    using System.Threading.Tasks;
    using ProjectNotifier.XPlace.Core;


    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        public static LoginViewModel Instance => new LoginViewModel()
        {

        };
     
        #region Private fields

        private ViewAnimation _unloadAnimation = ViewAnimation.SlideOutToBottom;

        private readonly ProjectLoader _projectLoader;

        #endregion


        #region Public properties

        public ViewAnimation UnloadAnimation
        {
            get => _unloadAnimation;
            set
            {
                _unloadAnimation = value;
                OnPropertyChanged();
            }
        } 
        #endregion


        #region Commands

        public RelayCommand GotoRegisterPageCommand { get; }

        public RelayCommand LoginCommand { get; }

        #endregion


        private LoginViewModel() { }

        public LoginViewModel(ProjectLoader projectLoader)
        {
            _projectLoader = projectLoader;

            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
        }


        #region Command callbacks
        
        private void ExecuteLoginCommand()
        {
            // Do login stuff 

            // Move page out of view
            UnloadAnimation = ViewAnimation.SlideOutToTop;

            DI.GetService<MainWindowViewModel>().CurrentPage = new ProjectsView()
            {
                ViewModel = new ProjectViewModel()
                {
                    ProjectList = _projectLoader.LoadProjects().ProjectList,
                },
            };
        }

        
        public void ExecuteGotoRegisterPageCommand()
        {
            // Change view
            DI.GetService<MainWindowViewModel>().CurrentPage = new RegisterView()
            {
                ViewModel = new RegisterViewModel()
            };
        }

        #endregion

    };
};
