namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;


    /// <summary>
    /// 
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {

        public static RegisterViewModel Intance => new RegisterViewModel()
        {

        };


        #region Commands

        public RelayCommand GotoLoginPageCommand { get; }

        #endregion


        public RegisterViewModel()
        {
            GotoLoginPageCommand = new RelayCommand(ExecuteGotoLoginPageCommand);
        }

        #region Command callbacks



        private void ExecuteGotoLoginPageCommand()
        {
            DI.GetService<MainWindowViewModel>().CurrentPage = new LoginView(new LoginViewModel());
        }

        #endregion

    };
};
