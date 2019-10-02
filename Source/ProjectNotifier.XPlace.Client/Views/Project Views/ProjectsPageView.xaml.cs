namespace ProjectNotifier.XPlace.Client
{
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectsPageView : BaseView<ProjectsPageViewModel>
    {
        public ProjectsPageView()
        {
            ViewLoadAnimation = ViewAnimation.FadeIn;

            InitializeComponent();
        }
    };
};
