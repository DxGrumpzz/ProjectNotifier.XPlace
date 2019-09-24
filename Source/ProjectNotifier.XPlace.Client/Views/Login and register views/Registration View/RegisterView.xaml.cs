namespace ProjectNotifier.XPlace.Client
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        public RegisterView()
        {
            InitializeComponent();
        }

        public RegisterView(RegisterViewModel viewModel) :
            this()
        {
            DataContext = viewModel;
        }
    };
};
