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
        public ViewAnimation ViewLoadAnimation { get; set; }

        /// <summary>
        /// An animtion that will happen when the view gets changes (to a different view)
        /// </summary>
        public ViewAnimation ViewUnloadAnimation { get; set; }


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


        private void View_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void View_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


        public async Task AnimateIn()
        {
            switch (ViewLoadAnimation)
            {

                case ViewAnimation.SlideInFromTop:
                {

                    Storyboard storyboard = new Storyboard();

                    // The slide in animation 
                    DoubleAnimation doubleAnimation = new DoubleAnimation()
                    {
                        Duration = TimeSpan.FromSeconds(LoadInAnimtaionInSeconds),

                        From = -ActualHeight,
                        To = 0,

                        DecelerationRatio = 0.9,
                    };

                    // Storyboard.SetTargetProperty(this, new PropertyPath("(Border.RenderTransform).(TranslateTransform.Y)"));
                    // Set the property to animate
                    Storyboard.SetTargetProperty(this, new PropertyPath("(UserControl.RenderTransform).(TranslateTransform.Y)"));

                    // Add animation to storyboard timeline
                    storyboard.Children.Add(doubleAnimation);

                    // Do animation
                    storyboard.Begin();

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
