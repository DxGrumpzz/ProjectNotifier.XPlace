namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using ProjectNotifier.XPlace.Core;


    /// <summary>
    /// 
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {

        public static RegisterViewModel Intance => new RegisterViewModel()
        {
        };


        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _registerWorking;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _waitForUnloadAnimation;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ViewAnimation _viewUnloadAnimation = ViewAnimation.SlideOutToBottom;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _username = "";

        #endregion


        #region Public properties

        /// <summary>
        /// A flag that indicates if the register view is working
        /// </summary>
        public bool RegisterWorking
        {
            get => _registerWorking;
            private set
            {
                _registerWorking = value;
                OnPropertyChanged();
            }
        }

        public bool WaitForUnloadAnimation
        {
            get => _waitForUnloadAnimation;
            set
            {
                _waitForUnloadAnimation = value;
                OnPropertyChanged();
            }
        }

        public ViewAnimation ViewUnloadAnimation
        {
            get => _viewUnloadAnimation;
            set
            {
                _viewUnloadAnimation = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// The user's chosen username
        /// </summary>
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

        public RelayCommand GotoLoginPageCommand { get; }
        public RelayCommand<IHaveMultiplePassword> RegisterCommand { get; }

        #endregion


        public RegisterViewModel()
        {
            GotoLoginPageCommand = new RelayCommand(ExecuteGotoLoginPageCommand);
            RegisterCommand = new RelayCommand<IHaveMultiplePassword>(ExecuteRegisterCommand);
        }




        #region Command callbacks

        private async Task ExecuteRegisterCommand(IHaveMultiplePassword passwords)
        {
            await RunCommandAsync(async () =>
            {
                RegisterWorking = true;

                // Change to login page
                WaitForUnloadAnimation = true;
                ViewUnloadAnimation = ViewAnimation.SlideOutToTop;

#if DEBUG == TRUE
                await Task.Delay(3000);
#endif

                DI.GetService<MainWindowViewModel>().CurrentPage = new LoginView()
                {
                    ViewModel = new LoginViewModel(DI.GetService<IProjectLoader>())
                };
            });
        }

        private void ExecuteGotoLoginPageCommand()
        {
            // Change to login page
            DI.GetService<MainWindowViewModel>().CurrentPage = new LoginView()
            {
                ViewModel = new LoginViewModel(DI.GetService<IProjectLoader>())
            };
        }

        #endregion

    };
};
