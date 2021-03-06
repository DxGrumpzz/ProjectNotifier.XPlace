﻿namespace ProjectNotifier.XPlace.Client
{
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;

    public class BaseView : UserControl
    {

        #region Private fields

        private object _viewModel;
     
        #endregion


        #region Public properties

        /// <summary>
        /// An animation that will happen when the page loads
        /// </summary>
        public ViewAnimation ViewUnloadAnimation
        {
            get => (ViewAnimation)GetValue(ViewUnloadAnimationProperty);
            set => SetValue(ViewUnloadAnimationProperty, value);
        }

        /// <summary>
        /// An animtion that will happen when the view gets changes (to a different view)
        /// </summary>
        public ViewAnimation ViewLoadAnimation
        {
            get => (ViewAnimation)GetValue(ViewLoadAnimationProperty);
            set => SetValue(ViewLoadAnimationProperty, value);
        }


       


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


        
        /// <summary>
        /// A flag that indicates if the unload animation should be awaited before switching to next page
        /// </summary>
        public bool WaitForUnloadAnimation
        {
            get => (bool)GetValue(WaitForUnloadAnimationProperty);
            set => SetValue(WaitForUnloadAnimationProperty, value);
        }

        #endregion


        public BaseView()
        {
            // Bind page events
            Loaded += View_Loaded;
            Unloaded += View_Unloaded;
        }

        public BaseView(object viewModel)
            : this()
        {
            ViewModel = viewModel;
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

                case ViewAnimation.FadeIn:
                {
                    // Do load in animation
                    await this.FadeInAsync(LoadInAnimtaionInSeconds);

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

                case ViewAnimation.FadeOut:
                {
                    // Do load in animation
                    await this.FadeOutAsync(LoadInAnimtaionInSeconds);
                    break;
                };

                // If the view animation is set to none
                default:
                    // Don't do anything
                    return;
            };
        }



        #region Dependency properties

        /// <summary>
        /// Dependency property for <see cref="ViewLoadAnimation"/>
        /// </summary>
        public static readonly DependencyProperty ViewLoadAnimationProperty =
            DependencyProperty.Register(
                nameof(ViewLoadAnimation),
                typeof(ViewAnimation),
                typeof(BaseView),
                new UIPropertyMetadata(ViewAnimation.None));


        /// <summary>
        /// Dependency property for <see cref="ViewUnloadAnimation"/>
        /// </summary>
        public static readonly DependencyProperty ViewUnloadAnimationProperty =
            DependencyProperty.Register(
                nameof(ViewUnloadAnimation),
                typeof(ViewAnimation),
                typeof(BaseView),
                new UIPropertyMetadata(ViewAnimation.None));

        /// <summary>
        /// dependency property for <see cref="WaitForUnloadAnimation"/>
        /// </summary>
        public static readonly DependencyProperty WaitForUnloadAnimationProperty =
            DependencyProperty.Register(
                nameof(WaitForUnloadAnimation),
                typeof(bool),
                typeof(BaseView),
                new PropertyMetadata(false));

        #endregion


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