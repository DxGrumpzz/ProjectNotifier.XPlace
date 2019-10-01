namespace ProjectNotifier.XPlace.Client
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public class BaseView : UserControl
    {

        #region Private properties


        public object _viewModel;

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
        public object ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = value;
            }
        }


        /// <summary>
        /// A flag that indicates if this view should animate out when it is loaded
        /// </summary>
        public bool ShouldAnimateOutOnLoad { get; set; }

        #endregion


        public BaseView()
        {
            // Bind page events
            Loaded += View_Loaded;
            Unloaded += View_Unloaded;
        }

        public BaseView(object viewModel)
            // Call default constructor
            : this()
        {
            ViewModel = viewModel;
        }

        private async void View_Loaded(object sender, RoutedEventArgs e)
        {
            if (ShouldAnimateOutOnLoad == true)
            {
                await AnimateOut();
            }
            else
            {
                await AnimateIn();
            };
        }

        private async void View_Unloaded(object sender, RoutedEventArgs e)
        {
            await AnimateOut();
        }


        /// <summary>
        /// Animates the page loading
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Animates the page unloading
        /// </summary>
        /// <returns></returns>
        public async Task AnimateOut()
        {
            switch (ViewUnloadAnimation)
            {
                case ViewAnimation.SlideOutToBottom:
                {
                    await this.SlideOutToBottomAsync(UnloadAnimtaionInSeconds);
                    break;
                };

                case ViewAnimation.SlideOutToTop:
                {
                    await this.SlideOutToTopAsync(UnloadAnimtaionInSeconds);
                    break;
                };

                // If the view animation is set to none
                default:
                    // Don't do anything
                    return;
            };
        }

    };


    public class BaseView<TViewModel> : BaseView
        where TViewModel : BaseViewModel
    {

        #region Public properties

        /// <summary>
        /// A generic overload of <see cref="BaseView.ViewModel"/>
        /// </summary>
        public new TViewModel ViewModel
        {
            get => (TViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        #endregion

        public BaseView() :
            base()
        { 
        }

        public BaseView(TViewModel viewModel) :
            base(viewModel)
        {
        }
    };
};