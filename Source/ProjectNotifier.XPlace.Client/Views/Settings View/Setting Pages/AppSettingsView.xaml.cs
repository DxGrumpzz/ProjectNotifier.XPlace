namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for AppSettingsPage.xaml
    /// </summary>
    public partial class AppSettingsView : BaseView
    {
        public AppSettingsView()
        {
            ViewLoadAnimation = ViewAnimation.FadeIn;

            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var s = DI.GetService<MainWindowViewModel>().CurrentPage.ViewModel;

            ((ProjectsPageViewModel)s).SettingsViewModel.CurrentSettingsPage = new SettingsListView()
            {
                DataContext = new SettingsListViewModel(),
            };
        }
    };
};
