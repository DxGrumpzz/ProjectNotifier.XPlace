namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class FirstView : UserControl
    {
        public FirstView()
        {
            InitializeComponent();
        }


        #region Public properties

        /// <summary>
        /// Currently displayed page
        /// </summary>
        public FirstViewViews CurrentPage
        {
            get => (FirstViewViews)GetValue(CurrentPageProperty);
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
                typeof(FirstView),
                new PropertyMetadata(null, null, ViewModelPropertyChanged));

       

        public static readonly DependencyProperty CurrentPageProperty =
            DependencyProperty.Register(
                nameof(CurrentPage),
                typeof(FirstViewViews),
                typeof(FirstView),
                new PropertyMetadata(FirstViewViews.Login, null, CurrentPageCallback));

        #endregion


        private static object CurrentPageCallback(DependencyObject d, object baseValue)
        {
            // Set ViewPresenters new page content, Without setting the viewmodel because
            // this method is only called when the view changes 
            (d as FirstView).ViewPresenter.Content = ((FirstViewViews)baseValue).ToFirstView();

            return baseValue;
        }

        private static object ViewModelPropertyChanged(DependencyObject d, object baseValue)
        {
            if(baseValue is null)
                return baseValue;

            // Set ViewPresenters new page content, and set the viewmodel
            (d as FirstView).ViewPresenter.Content = (d as FirstView).CurrentPage.ToFirstView(baseValue);

            return baseValue;
        }
    };
};
