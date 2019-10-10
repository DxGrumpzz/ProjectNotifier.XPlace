namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Diagnostics;
    using System.Security;
    using System.Threading.Tasks;
    using System.Windows;


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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _loginWorking;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _username;

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


        public bool LoginWorking
        {
            get => _loginWorking;
            set
            {
                _loginWorking = value;
                OnPropertyChanged();
            }
        }


        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands

        public RelayCommand GotoRegisterPageCommand { get; }

        public RelayCommand<IHavePassword> LoginCommand { get; }

        #endregion


        public LoginViewModel(IProjectLoader projectLoader)
        {
            _projectLoader = projectLoader;

            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
            LoginCommand = new RelayCommand<IHavePassword>(ExecuteLoginCommandAsync);
        }


        #region Command callbacks

        private async Task ExecuteLoginCommandAsync(IHavePassword password)
        {
            await RunCommandAsync(async () =>
            {
                LoginWorking = true;

                // Move page out of view
                UnloadAnimation = ViewAnimation.SlideOutToTop;
                WaitForUnloadAnimation = true;

#if DEBUG == TRUE
                // Fake working thing
                await Task.Delay(2000);
#endif

                // Change to projects view
                DI.GetService<MainWindowViewModel>().CurrentPage = new ProjectsPageView()
                {
                    ViewModel = new ProjectsPageViewModel()
                    {
                        ProjectList = await _projectLoader.LoadProjectsAsObservableAsync(),
                    },
                };
            });
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