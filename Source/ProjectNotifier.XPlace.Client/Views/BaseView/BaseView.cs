namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public class BaseView : UserControl
    {

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

        #endregion


        public BaseView()
        {
            // Bind page events
            Loaded += View_Loaded;
            Unloaded += View_Unloaded;
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
                    Storyboard storyboard = new Storyboard();

                    // The slide in animation 
                    ThicknessAnimation doubleAnimation = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(LoadInAnimtaionInSeconds),

                        From = new Thickness(0, -ActualHeight, 0, ActualHeight),
                        To = new Thickness(0),

                        DecelerationRatio = 0.9,
                    };

                    // Set the property to animate
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Margin"));

                    // Add animation to storyboard timeline
                    storyboard.Children.Add(doubleAnimation);

                    // Do animation
                    storyboard.Begin(this);

                    // Wait for the animation to finish
                    await Task.Delay(TimeSpan.FromSeconds(LoadInAnimtaionInSeconds));

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
                    Storyboard storyboard = new Storyboard();

                    // The slide in animation 
                    ThicknessAnimation doubleAnimation = new ThicknessAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(LoadInAnimtaionInSeconds),

                        From = new Thickness(0),
                        To = new Thickness(0, ActualHeight, 0, -ActualHeight),

                        DecelerationRatio = 0.9,
                    };

                    // Set the property to animate
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Margin"));

                    // Add animation to storyboard timeline
                    storyboard.Children.Add(doubleAnimation);

                    // Do animation
                    storyboard.Begin(this);

                    // Wait for the animation to finish
                    await Task.Delay(TimeSpan.FromSeconds(LoadInAnimtaionInSeconds));

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
