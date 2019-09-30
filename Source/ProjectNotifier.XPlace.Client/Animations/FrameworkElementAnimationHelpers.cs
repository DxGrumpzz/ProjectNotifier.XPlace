namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Animation;


    /// <summary>
    /// A helpers class that can add storyboard animations to <see cref="FrameworkElement "/>
    /// </summary>
    public static class FrameworkElementAnimationHelpers
    {


        /// <summary>
        /// Slide a control in from the top
        /// </summary>
        /// <param name="control"> The control to animate </param>
        /// <param name="seconds"> How will the animation take place </param>
        /// <returns></returns>
        public static async Task SlideInFromTopAsync(this FrameworkElement control, float seconds)
        {
            Storyboard storyboard = new Storyboard();

            // Add slide in from top animation
            storyboard.AddSlideInFromTop(seconds, control.ActualHeight);

            // Begin animation
            storyboard.Begin(control);

            // Wait for the animation to finish
            await Task.Delay(TimeSpan.FromSeconds(seconds));
        }


        /// <summary>
        /// Slide a control out to the bottom
        /// </summary>
        /// <param name="view"> The control to animate </param>
        /// <param name="seconds"> How will the animation take place </param>
        /// <returns></returns>
        public static async Task SlideOutToBottomAsync(this FrameworkElement view, float seconds)
        {
            Storyboard storyboard = new Storyboard();

            // Add slide in from top animation
            storyboard.AddSlideIOutToBottom(seconds, view.ActualHeight);

            // Begin animation
            storyboard.Begin(view);

            // Wait for the animation to finish
            await Task.Delay(TimeSpan.FromSeconds(seconds));
        }


        /// <summary>
        /// Slide a control in from the top
        /// </summary>
        /// <param name="control"> The control to animate </param>
        /// <param name="seconds"> How will the animation take place </param>
        /// <returns></returns>
        public static async Task SlideOutToTopAsync(this FrameworkElement control, float seconds)
        {
            Storyboard storyboard = new Storyboard();

            // Add slide in from top animation
            storyboard.AddSlideOutToTop(seconds, control.ActualHeight);

            // Begin animation
            storyboard.Begin(control);

            // Wait for the animation to finish
            await Task.Delay(TimeSpan.FromSeconds(seconds));
        }
    };
};