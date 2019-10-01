namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ViewAnimation _unloadAnimation = ViewAnimation.SlideOutToBottom;
        
        private readonly ProjectLoader _projectLoader;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _waitForUnloadAnimation;

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
            WaitForUnloadAnimation = true;


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
