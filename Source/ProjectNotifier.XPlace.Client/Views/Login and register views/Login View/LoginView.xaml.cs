namespace ProjectNotifier.XPlace.Client
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : BaseView<LoginViewModel>
	{
		public LoginView()
		{
			InitializeComponent();
		}

        public LoginView(LoginViewModel viewModel) : 
            base(viewModel)
        {
            InitializeComponent();
        }
    };
};
