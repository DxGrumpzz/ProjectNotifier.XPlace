namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR.Client;
    using System.Net;
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

        private readonly AppSettingsDataModel _settings;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private bool _loginWorking;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _username = "";

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


        public LoginViewModel()
        {
            _settings = DI.ClientAppSettings();

            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
            LoginCommand = new RelayCommand<IHavePassword>(ExecuteLoginCommandAsync);
        }


        #region Command callbacks

        private async Task ExecuteLoginCommandAsync(IHavePassword password)
        {
            await RunCommandAsync(() => LoginWorking,
            async () =>
            {
                await DI.GetService<ISignInManager>().SignInAsync(Username, password.Password,
                signSuccessfull: async (response) =>
                {
                    // Build and start projects hub connection
                    await DI.GetService<IServerConnection>().StartHubConnectionAsync("Https://LocalHost:5001/ProjectsHub", DI.GetService<IServerConnection>().Cookies);

                    // Save cookie
                    await DI.GetService<IClientDataStore>().SaveLoginCredentialsAsync(new LoginCredentialsDataModel()
                    {
                        DataModelID = Guid.NewGuid().ToString(),
                        Cookie = DI.GetService<IServerConnection>().Cookies.GetCookieHeader(new Uri("Https://LocalHost:5001"))
                    });

                    // Read response content
                    var responseContent = await response.Content.ReadAsAsync<LoginResponseModel>();

                    // Update cache
                    DI.GetService<IClientCache>().ProjectListCache = responseContent.Projects;

                    // Save user profile 
                    DI.GetService<IClientDataStore>().SaveUserProfile(responseContent.UserModel);

                    // Move page out of view
                    UnloadAnimation = ViewAnimation.SlideOutToTop;
                    WaitForUnloadAnimation = true;


                    DI.GetService<ProjectsPageViewModel>().ProjectList = new ObservableCollection<ProjectItemViewModel>(responseContent.Projects
                    .Select((p) => new ProjectItemViewModel()
                    {
                        ProjectModel = p,
                    })
                    .Take(10));

                    // Change to projects view
                    DI.GetService<MainWindowViewModel>().CurrentPage = new ProjectsPageView()
                    {
                        ViewModel = DI.GetService<ProjectsPageViewModel>(),
                    };
                },
                signInFailed: async (response) =>
                {
                    // Remove working overlay
                    LoginWorking = false;

                    // Display sign-in error
                    ErrorText = await response.Content.ReadAsStringAsync();

                    // Display error
                    await ShowErrorDisplay();
                });
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


        #region Private helpers

        /// <summary>
        /// Show the error display and automatically closed
        /// </summary>
        /// <returns></returns>
        private async Task ShowErrorDisplay()
        {
            // Show error display
            HasError = true;

            // wait a bit
            await Task.Delay(3000);

            // Close
            HasError = false;
        }

        #endregion
    };
};