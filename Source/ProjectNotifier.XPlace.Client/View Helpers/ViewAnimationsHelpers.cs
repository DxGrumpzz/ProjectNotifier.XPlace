namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public static class ViewAnimationsHelpers
    {

        /// <summary>
        /// Slide a control in from the top
        /// </summary>
        /// <param name="view"> The control to animate </param>
        /// <param name="seconds"> How will the animation take place </param>
        /// <returns></returns>
        public static async Task SlideInFromTopAsync(this UserControl view, float seconds)
        {
            Storyboard storyboard = new Storyboard();

            // Add slide in from top animation
            storyboard.AddSlideInFromTop(seconds, view.ActualHeight);

            // Begin animation
            storyboard.Begin(view);

            // Wait for the animation to finish
            await Task.Delay(TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// Slide a control out to the bottom
        /// </summary>
        /// <param name="view"> The control to animate </param>
        /// <param name="seconds"> How will the animation take place </param>
        /// <returns></returns>
        public static async Task SlideOutToBottomAsync(this UserControl view, float seconds)
        {
            Storyboard storyboard = new Storyboard();

            // Add slide in from top animation
            storyboard.AddSlideIOutToBottom(seconds, view.ActualHeight);

            // Begin animation
            storyboard.Begin(view);

            // Wait for the animation to finish
            await Task.Delay(TimeSpan.FromSeconds(seconds));
        }
    };
};
