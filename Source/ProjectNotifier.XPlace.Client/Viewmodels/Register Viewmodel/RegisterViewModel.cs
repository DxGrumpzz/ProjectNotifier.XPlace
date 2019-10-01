namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Threading.Tasks;


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
            // Change to login page
            DI.GetService<MainWindowViewModel>().CurrentPage = new LoginView()
            {
                ViewModel = new LoginViewModel(DI.GetService<ProjectLoader>())
            };
        }

        #endregion

    };
};
