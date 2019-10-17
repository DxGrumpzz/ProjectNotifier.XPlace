namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;


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

        private readonly ClientAppSettingsModel _settings;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _loginWorking;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _username;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _hasError;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _errorText;

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

        public bool HasError
        {
            get => _hasError;
            set
            {
                _hasError = value;
                OnPropertyChanged();
            }
        }

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

        public RelayCommand GotoRegisterPageCommand { get; }

        public RelayCommand<IHavePassword> LoginCommand { get; }

        #endregion


        public LoginViewModel(ClientAppSettingsModel settings)
        {
            _settings = settings;

            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
            LoginCommand = new RelayCommand<IHavePassword>(ExecuteLoginCommandAsync);
        }


        #region Command callbacks

        private async Task ExecuteLoginCommandAsync(IHavePassword password)
        {
            await RunCommandAsync(() => LoginWorking,
            async () =>
            {

                HttpClient httpClient = DI.GetService<HttpClient>();

                var response = await httpClient.PostAsJsonAsync("https://localhost:5001/Account/Login",
                new LoginRequestModel()
                {
                    Username = Username,
                    Password = password.Password.Unsecure(),
                });


                if (response.IsSuccessStatusCode == false)
                {
                    // Remove working overlay
                    LoginWorking = false;

                    // Display error
                    HasError = true;
                    HasError = false;

                    // Display sign-in error
                    ErrorText = "שם משתמש או סיסמא לא נכונים";
                }
                else
                {
                    // Build hub connection
                    await (DI.GetService<IServerConnection>().ProjectsHubConnection =
                    new HubConnectionBuilder()
                    // Connect to project hub url
                    .WithUrl("Https://LocalHost:5001/ProjectsHub")
                    // Build hub connection
                    .Build())
                    // Start the connection
                    .StartAsync();

                    // Read response content
                    var responseContent = await response.Content.ReadAsAsync<LoginResponseModel>();


                    // Move page out of view
                    UnloadAnimation = ViewAnimation.SlideOutToTop;
                    WaitForUnloadAnimation = true;


                    // Change to projects view
                    DI.GetService<MainWindowViewModel>().CurrentPage = new ProjectsPageView()
                    {
                        ViewModel = new ProjectsPageViewModel()
                        {
                            // Convert the list of IEnumerable to an ObservableCollection
                            ProjectList = new ObservableCollection<ProjectItemViewModel>(responseContent.Projects
                            .Select((p) => new ProjectItemViewModel()
                            {
                                ProjectModel = p,
                            })
                            .AsEnumerable()
                            .Take(_settings.ProjectsToDisplay))
                        },
                    };
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