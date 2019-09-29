namespace ProjectNotifier.XPlace.Client
{
    using System.Threading.Tasks;
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

        /// <summary>
        /// The current view's viewmodel
        /// </summary>
        //public BaseViewModel ViewModel
        //{
            //get => (BaseViewModel)GetValue(ViewModelProperty);
            //set => SetValue(ViewModelProperty, value);
        //}

        #endregion

        #region Dependency properties


        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(BaseView),
                typeof(MainPageHost),
                new UIPropertyMetadata(null, CurrentPageChanged));


        //public static readonly DependencyProperty ViewModelProperty =
        //    DependencyProperty.Register(
        //        nameof(ViewModel),
        //        typeof(BaseViewModel),
        //        typeof(MainPageHost),
        //        new UIPropertyMetadata(null, null, ViewModelPropertyChanged));

        #endregion



        private async static void CurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Get current view presenters
            var newPageView = (d as MainPageHost).NewView;
            var oldPageView = (d as MainPageHost).OldView;

            // Store NewPage's content
            var oldPageContent = newPageView.Content;

            // Remove current NewPage content
            //newPageView.Content = null;

            // Set old PageContent to what used to be the NewPage
            //oldPageView.Content = oldPageContent;


            //// Use the ViewUnloadAnimation to animate out the old page
            if (oldPageContent is BaseView oldPage)
            {
                // Animate old page out of view
                await oldPage.AnimateOut();
            };

            // Set new page's content
            newPageView.Content = e.NewValue;
        }


        //    private static object ViewModelPropertyChanged(DependencyObject d, object baseValue)
        //    {
        //        var newPageView = (d as MainPageHost).NewView.Content;
        //        var oldPageView = (d as MainPageHost).OldView.Content;

        //        if ((d as MainPageHost).NewView.Content is BaseView baseView)
        //        {
        //            baseView.DataContext = baseValue;
        //        };

        //        return baseValue;
        //    }
    }
}