namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Threading.Tasks;
    using ProjectNotifier.XPlace.Core;


    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        public static LoginViewModel Instance => new LoginViewModel()
        {

        };

        private ViewAnimation _unloadAnimation = ViewAnimation.SlideOutToBottom;

        public ViewAnimation  UnloadAnimation
        {
            get => _unloadAnimation;
            set
            {
                _unloadAnimation = value;
                OnPropertyChanged();
            }
        }


        #region Commands

        public RelayCommand GotoRegisterPageCommand { get; }

        public RelayCommand LoginCommand { get; }

        #endregion


        public LoginViewModel()
        {
            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
            LoginCommand = new RelayCommand(ExecuteLoginCommand);
        }

        #region Command callbacks
        
        private void ExecuteLoginCommand()
        {
            // Do login stuff 

            // Move page out of view
            UnloadAnimation = ViewAnimation.SlideOutToTop;

            DI.GetService<MainWindowViewModel>().CurrentPage = new RegisterView()
            {
                ViewModel = new RegisterViewModel()
            };
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
