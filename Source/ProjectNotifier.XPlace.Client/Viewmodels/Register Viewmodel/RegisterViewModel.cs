namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;
    using System.Net.Http;
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _hasError;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _errorText;

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


        /// <summary>
        /// A boolean flag that indicates if the registration failed
        /// </summary>
        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Contains the registration error
        /// </summary>
        public string ErrorText
        {
            get => _errorText;
            set
            {
                _errorText = value;
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
            await RunCommandAsync(() => RegisterWorking,
            async () =>
            {

                // Send a registration request to the server
                HttpClient client = DI.GetService<HttpClient>();

                var response = await client.PostAsJsonAsync("https://localhost:5001/Account/Register", 
                new RegisterModel()
                {
                    Username = Username,
                    Password = passwords.Password.Unsecure(),
                    ConfirmationPassword = passwords.ConfirmPassword.Unsecure(),
                });


                // If registration was unsuccesful
                if (response.IsSuccessStatusCode == false)
                {
                    // Remove working overlay
                    RegisterWorking = false;

                    // Display error overlay
                    HasError = true;
                    HasError = false;

                    // Set text
                    ErrorText = await response.Content.ReadAsStringAsync();
                }
                // If registration was succesful
                else
                {
                    // Change to login page
                    WaitForUnloadAnimation = true;
                    ViewUnloadAnimation = ViewAnimation.SlideOutToTop;

                    // Move to login page
                    DI.GetService<MainWindowViewModel>().CurrentPage = new LoginView()
                    {
                        ViewModel = new LoginViewModel(DI.GetService<ClientAppSettingsModel>())
                        {
                            Username = Username,
                        },
                    };
                };
            });
        }

        private void ExecuteGotoLoginPageCommand()
        {
            // Change to login page
            DI.GetService<MainWindowViewModel>().CurrentPage = new LoginView()
            {
                ViewModel = new LoginViewModel(DI.GetService<ClientAppSettingsModel>())
            };
        }

        #endregion

    };
};