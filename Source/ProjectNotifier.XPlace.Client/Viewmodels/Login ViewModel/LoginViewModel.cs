namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Threading.Tasks;


    /// <summary>
    /// 
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {

        public static LoginViewModel Instance => new LoginViewModel()
        {

        };

        #region Private properties

        private bool _slideDown;
        private bool _slideDownFromTop;

        #endregion


        public bool SlideDown
        {
            get => _slideDown;
            set
            {
                _slideDown = value;
                OnPropertyChanged();
            }
        }

        public bool SlideDownFromTop
        {
            get => _slideDownFromTop;
            set
            {
                _slideDownFromTop = value;
                OnPropertyChanged();
            }
        }


        #region Commands

        public RelayCommand<TextEntryControl> GotoRegisterPageCommand { get; }

        #endregion


        public LoginViewModel()
        {
            //SlideDownFromTop = true;

            GotoRegisterPageCommand = new RelayCommand<TextEntryControl>(ExecuteGotoRegisterPageCommand);
        }


        #region Command callbacks

        private void ExecuteGotoRegisterPageCommand(TextEntryControl param)
        {
            // SlideDown = true;
            // 
            // // Wait for animaation to finish
            // await Task.Delay(200);

            // Change view
            //DI.GetService<MainWindowViewModel>().CurrentPage = new RegisterView(new RegisterViewModel());
        }

        #endregion

    };
};
