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

            DataContext = viewModel;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await AnimateOut();
        }
    };
};
