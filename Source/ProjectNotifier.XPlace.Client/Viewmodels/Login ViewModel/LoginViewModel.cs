using ProjectNotifier.XPlace.Core;
using System.Threading.Tasks;

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

        private bool _slideDown;

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

        public RelayCommand GotoRegisterPageCommand { get; }

        #endregion


        public LoginViewModel()
        {
            GotoRegisterPageCommand = new RelayCommand(ExecuteGotoRegisterPageCommand);
        }


        #region Command callbacks

        private async Task ExecuteGotoRegisterPageCommand()
        {
            SlideDown = true;

            // Wait for animaation to finish
            await Task.Delay(200);

            // Change view
            DI.GetService<MainWindowViewModel>().CurrentPage = new RegisterView(new RegisterViewModel());
        }

        #endregion

    };
};
