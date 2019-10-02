namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Linq;
    using System.Collections.ObjectModel;
    using System;


    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        public static LoginViewModel Instance => new LoginViewModel()
        {

        };


        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ViewAnimation _unloadAnimation = ViewAnimation.SlideOutToBottom;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _waitForUnloadAnimation;

        private readonly IProjectLoader _projectLoader;

        #endregion


        #region Public properties

        /// <summary>
        /// Which animation this page will use when changing to a different page, 
        /// Unload animation for register and Projects view is different
        /// </summary>
        public ViewAnimation UnloadAnimation
        {
            get => _unloadAnimation;
            set
            {
                _unloadAnimation = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Should the page host wait for the unload animation to finish before changing to a different page
        /// </summary>
        public bool WaitForUnloadAnimation
        {
            get => _waitForUnloadAnimation;
            set
            {
                _waitForUnloadAnimation = value;
                OnPropertyChanged();
            }
        }


        #endregion


        #region Commands

        public RelayCommand GotoRegisterPageCommand { get; }

        public RelayCommand LoginCommand { get; }

        #endregion


        private LoginViewModel() { }

        public LoginViewModel(IProjectLoader projectLoader)
        {
            _projectLoader = projectLoader;

            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
            LoginCommand = new RelayCommand(ExecuteLoginCommandAsync);
        }


        #region Command callbacks

        private async Task ExecuteLoginCommandAsync()
        {
            // Do login stuff 


            // Move page out of view
            UnloadAnimation = ViewAnimation.SlideOutToTop;
            WaitForUnloadAnimation = true;


            var projects = (await _projectLoader.LoadProjectsAsync())
                .Select(project => new ProjectItemViewModel()
                {
                    ProjectModel = project,
                });
         
            DI.GetService<MainWindowViewModel>().CurrentPage = new ProjectsPageView()
            {
                ViewModel = new ProjectsPageViewModel()
                {
                    ProjectList = new ObservableCollection<ProjectItemViewModel>(projects),
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