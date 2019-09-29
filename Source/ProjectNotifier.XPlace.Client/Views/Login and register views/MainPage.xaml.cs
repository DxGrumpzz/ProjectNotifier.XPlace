namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }


        #region Public properties

        /// <summary>
        /// Currently displayed page
        /// </summary>
        public MainPageViews CurrentPage
        {
            get => (MainPageViews)GetValue(CurrentPageProperty);
            set => SetValue(CurrentPageProperty, value);
        }

        /// <summary>
        /// The current view's viewmodel
        /// </summary>
        public BaseViewModel ViewModel
        {
            get => (BaseViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        #endregion

        #region Dependency properties

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(
                nameof(ViewModel),
                typeof(BaseViewModel),
                typeof(MainPage),
                new PropertyMetadata(null, null, ViewModelPropertyChanged));



        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(MainPageViews),
                typeof(MainPage),
                new PropertyMetadata(MainPageViews.Login, null, CurrentPageCallback));

        #endregion


        private static object CurrentPageCallback(DependencyObject d, object baseValue)
        {
            // Set ViewPresenters new page content, Without setting the viewmodel because
            // this method is only called when the view changes 
            (d as MainPage).ViewPresenter.Content = ((MainPageViews)baseValue).ToMainPage();

            return baseValue;
        }

        private static object ViewModelPropertyChanged(DependencyObject d, object baseValue)
        {
            // Set ViewPresenters new page content, and set the viewmodel
            (d as MainPage).ViewPresenter.Content = (d as MainPage).CurrentPage.ToMainPage(baseValue);

            return baseValue;
        }
    };
};
