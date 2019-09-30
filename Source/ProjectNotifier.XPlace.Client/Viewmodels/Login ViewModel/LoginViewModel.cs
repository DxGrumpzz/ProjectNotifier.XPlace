namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;


    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        public static LoginViewModel Instance => new LoginViewModel()
        {

        };


        #region Commands

        public RelayCommand GotoRegisterPageCommand { get; }

        #endregion


        public LoginViewModel()
        {
            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
        }


        #region Command callbacks

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
