namespace ProjectNotifier.XPlace.Client
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainPageHost.xaml
    /// </summary>
    public partial class MainPageHost : UserControl
    {

        public MainPageHost()
        {
            InitializeComponent();
        }


        #region Public properties

        /// <summary>
        /// Currently displayed page
        /// </summary>
        public BaseView CurrentPage
        {
            get => (BaseView)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        #endregion


        #region Dependency properties

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(BaseView),
                typeof(MainPageHost),
                new UIPropertyMetadata(null, CurrentPageChanged));

        #endregion


        private static void CurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Get current view presenters
            var newPageView = (d as MainPageHost).NewView;
            var oldPageView = (d as MainPageHost).OldView;

            // Store NewPage's content
            var oldPageContent = newPageView.Content;

            // Set old PageContent to what used to be the NewPage
            oldPageView.Content = oldPageContent;


            // Use the ViewUnloadAnimation to animate out the old page
            if (oldPageContent is BaseView oldPage)
            {
                oldPage.ShouldAnimateOutOnLoad = true;
            };

            // Set new page's content
            newPageView.Content = e.NewValue;
        }
    }
}