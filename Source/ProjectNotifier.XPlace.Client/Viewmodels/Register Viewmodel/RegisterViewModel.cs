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

        #region Private properties

        private bool _slideDownFromTop;

        private bool _slideDown;

        #endregion


        public bool SlideDownFromTop
        {
            get => _slideDownFromTop;
            set
            {
                _slideDownFromTop = value;
                OnPropertyChanged();
            }
        }


        public bool SlideDown
        {
            get => _slideDown;
            set
            {
                _slideDown = value;
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



        private async Task ExecuteGotoLoginPageCommand()
        {
            // Slide control down
            SlideDown = true;

            // Wait for animation to finish
            await Task.Delay(200);

            // Change to login page
            DI.GetService<MainWindowViewModel>().CurrentPage = new LoginView(new LoginViewModel());
        }

        #endregion

    };
};
