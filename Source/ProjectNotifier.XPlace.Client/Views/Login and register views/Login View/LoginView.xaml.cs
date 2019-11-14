namespace ProjectNotifier.XPlace.Client
{
    using System.Security;
    using System.Windows;

    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : BaseView<LoginViewModel>, IHavePassword
	{
		public LoginView()
		{
            ViewLoadAnimation = ViewAnimation.SlideInFromTop;
            ViewUnloadAnimation= ViewAnimation.SlideOutToBottom;

            InitializeComponent();
		}

        public LoginView(LoginViewModel viewModel) : 
            base(viewModel)
        {
            InitializeComponent();
        }

        public SecureString Password => PasswordField.SecurePassword;

        public SecureString ConfirmPassword => null;
    };
};
