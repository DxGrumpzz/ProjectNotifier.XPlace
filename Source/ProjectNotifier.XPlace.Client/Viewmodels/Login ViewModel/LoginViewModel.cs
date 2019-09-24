using ProjectNotifier.XPlace.Core;

namespace ProjectNotifier.XPlace.Client
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        public static LoginViewModel Instance => new LoginViewModel()
        {

        };

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }


        #region Commands

        public RelayCommand GotoRegisterPageCommand { get; }

        #endregion


        public LoginViewModel()
        {
            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
        }


        #region Command callbacks

        private void ExecuteGotoRegisterPageCommand()
        {
            DI.GetService<MainWindowViewModel>().CurrentPage = new RegisterView(new RegisterViewModel());

        }

        #endregion

    };
};
