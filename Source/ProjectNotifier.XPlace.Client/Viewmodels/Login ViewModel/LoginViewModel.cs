namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Diagnostics;
    using System.Threading.Tasks;


    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        public static LoginViewModel Instance => new LoginViewModel(null)
        {

        };


        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ViewAnimation _unloadAnimation = ViewAnimation.SlideOutToBottom;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _waitForUnloadAnimation;

        private readonly IProjectLoader _projectLoader;

        private bool _loginWorking;

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


        public LoginViewModel(IProjectLoader projectLoader)
        {
            _projectLoader = projectLoader;

            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
            LoginCommand = new RelayCommand(ExecuteLoginCommandAsync);
        }


        #region Command callbacks

        private async Task ExecuteLoginCommandAsync()
        {
            if (_loginWorking == true)
                return;
            try
            {
                _loginWorking = true;

                // Do login stuff 



                // Move page out of view
                UnloadAnimation = ViewAnimation.SlideOutToTop;
                WaitForUnloadAnimation = true;


                // Change to projects view
                DI.GetService<MainWindowViewModel>().CurrentPage = new ProjectsPageView
                {
                    ViewModel = new ProjectsPageViewModel()
                    {
                        ProjectList = await _projectLoader.LoadProjectsAsObservableAsync(),
                    },
                };
            }
            finally
            {
                _loginWorking = false;
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