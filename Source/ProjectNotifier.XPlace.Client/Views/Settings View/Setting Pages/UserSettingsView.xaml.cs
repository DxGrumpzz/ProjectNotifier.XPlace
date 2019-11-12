namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for UserSettingsView.xaml
    /// </summary>
    public partial class UserSettingsView : BaseView
    {
        public UserSettingsView()
        {
            ViewLoadAnimation = ViewAnimation.FadeIn;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var s = DI.GetService<MainWindowViewModel>().CurrentPage.ViewModel;

            ((ProjectsPageViewModel)s).SettingsViewModel.CurrentSettingsPage = new SettingsListView()
            {
                DataContext = new SettingsListViewModel(),
            };
        }
    };
};
