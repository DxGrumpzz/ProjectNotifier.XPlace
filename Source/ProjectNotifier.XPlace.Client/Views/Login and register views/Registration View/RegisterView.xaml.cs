namespace ProjectNotifier.XPlace.Client
{
    using System.Security;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : BaseView<RegisterViewModel>, IHaveMultiplePassword
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        public RegisterView(RegisterViewModel viewModel) : 
            base(viewModel)
        {
            InitializeComponent();
        }

        public SecureString Password => PasswordFeid.SecurePassword;
        public SecureString ConfirmPassword => ConfirmedPasswordField.SecurePassword;
    };
};
