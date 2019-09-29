namespace ProjectNotifier.XPlace.Client
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public class BaseView<TViewModel> : UserControl
        where TViewModel : BaseViewModel
    {

        #region Private properties


        public TViewModel _viewModel;

        #endregion

        #region Public properties

        /// <summary>
        /// An animation that will happen when the page loads
        /// </summary>
        public ViewAnimation ViewLoadAnimation { get; set; } = ViewAnimation.SlideInFromTop;

        /// <summary>
        /// An animtion that will happen when the view gets changes (to a different view)
        /// </summary>
        public ViewAnimation ViewUnloadAnimation { get; set; } = ViewAnimation.SlideOutToBottom;


        /// <summary>
        /// How long the load animation takes to complete
        /// </summary>
        public float LoadInAnimtaionInSeconds { get; set; } = 0.5f;

        /// <summary>
        /// How long the unload animation takes to complete
        /// </summary>
        public float UnloadAnimtaionInSeconds { get; set; } = 0.5f;


        /// <summary>
        /// This view's associated viewmodel
        /// </summary>
        public TViewModel ViewModel 
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = value;
            }
        }

        #endregion


        public BaseView()
        {
            // Bind page events
            Loaded += View_Loaded;
            Unloaded += View_Unloaded;
        }

        public BaseView(TViewModel viewModel)
            : base()
        {
            ViewModel = viewModel;
            DataContext = viewModel;
        }

        private async void View_Unloaded(object sender, RoutedEventArgs e)
        {
            await AnimateOut();
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            await AnimateIn();
        }


        public async Task AnimateIn()
        {
            switch (ViewLoadAnimation)
            {
                case ViewAnimation.SlideInFromTop:
                {
                    // Do load in animation
                    await this.SlideInFromTopAsync(LoadInAnimtaionInSeconds);

                    break;
                };

                // If the view animation is set to none
                default:
                    // Don't do anything
                    return;
            };
        }

        public async Task AnimateOut()
        {
            switch (ViewUnloadAnimation)
            {
                case ViewAnimation.SlideOutToBottom:
                {
                    // Do animation
                    await this.SlideOutToBottomAsync(UnloadAnimtaionInSeconds);

                    break;
                };

                // If the view animation is set to none
                default:
                    // Don't do anything
                    return;
            };
        }

    }
}
