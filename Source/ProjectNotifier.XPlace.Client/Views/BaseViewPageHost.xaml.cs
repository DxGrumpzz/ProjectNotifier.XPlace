namespace ProjectNotifier.XPlace.Client
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for BaseView.xaml
    /// </summary>
    public partial class BaseViewPageHost : UserControl
    {
        public BaseViewPageHost()
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
                typeof(BaseViewPageHost),
                new UIPropertyMetadata(null, CurrentPageChanged));

        #endregion


        private async static void CurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // Get current view presenters
            var newPageView = (d as BaseViewPageHost).NewView;
            var oldPageView = (d as BaseViewPageHost).OldView;

            // If old paage value if null
            bool waitForUnloadAnimtaion = ((BaseView)e.OldValue) is null ?
                // Don't wait for load animation
                false :
                // Otherswise, get the actuall value for WaitForUnloadAnimation
                ((BaseView)e.OldValue).WaitForUnloadAnimation;

            // Store NewPage's content
            var oldPageContent = newPageView.Content;



            if (oldPageContent is BaseView oldPage)
            {
                if(oldPage.ViewUnloadAnimation == ViewAnimation.None)
                {
                    oldPage.Content = null;
                };


                // If WaitForAnimationToFinish is true
                if (waitForUnloadAnimtaion == true)
                {
                    // Animate the page out and wait for the animation to complete
                    await oldPage.AnimateOut();
                }
                else
                {
                    // Set old PageContent to what used to be the NewPage
                    oldPageView.Content = oldPageContent;

                    oldPage.ShouldAnimateOutOnLoad = true;
                }


            }

            // Set new page's content
            newPageView.Content = e.NewValue;
        }

    };
};