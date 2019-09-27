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


        private bool _slideDownFromTop;

        public bool SlideDownFromTop
        {
            get { return _slideDownFromTop; }
            set
            {
                _slideDownFromTop = value;
                OnPropertyChanged();
            }
        }


        #region Commands

        public RelayCommand GotoLoginPageCommand { get; }

        #endregion


        public RegisterViewModel()
        {
            SlideDownFromTop = true;

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
