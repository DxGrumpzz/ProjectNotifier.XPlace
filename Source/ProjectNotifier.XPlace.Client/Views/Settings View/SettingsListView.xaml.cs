namespace ProjectNotifier.XPlace.Client
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SettingIconView.xaml
    /// </summary>
    public partial class SettingsListView : BaseView<SettingsListViewModel>
    {
        public SettingsListView()
        {
            ViewLoadAnimation = ViewAnimation.FadeIn;

            InitializeComponent();
        }
    }
}
