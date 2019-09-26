namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media.Animation;

    public static class StoryboardHelpers
    {

        /// <summary>
        /// Adds a slide aanimation to a storyboard
        /// </summary>
        /// <param name="storyboard"> The storyboard </param>
        /// <param name="seconds"> How long the animation will take </param>
        /// <param name="offset"> The starting point where the animation will begin from </param>
        /// <param name="decelerationRatio"></param>
        /// <returns></returns>
        public static void AddSlideInFromTop(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            // The slide in animation 
            ThicknessAnimation doubleAnimation = new ThicknessAnimation()
            {
                Duration = TimeSpan.FromSeconds(seconds),

                From = new Thickness(0, -offset, 0, offset),
                To = new Thickness(0),

                DecelerationRatio = decelerationRatio,
            };

            // Set the property to animate
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Margin"));

            // Add animation to storyboard timeline
            storyboard.Children.Add(doubleAnimation);

        }


        /// <summary>
        /// Adds a slide out to bottom animation to a storyboard
        /// </summary>
        /// <param name="storyboard"> The storyboard </param>
        /// <param name="seconds"> How long the animation will take </param>
        /// <param name="offset"> The starting point where the animation will begin from </param>
        /// <param name="decelerationRatio"></param>
        /// <returns></returns>
        public static void AddSlideIOutToBottom(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            // The slide in animation 
            ThicknessAnimation doubleAnimation = new ThicknessAnimation()
            {
                Duration = TimeSpan.FromSeconds(seconds),

                From = new Thickness(0),
                To = new Thickness(0, offset, 0, -offset),

                DecelerationRatio = decelerationRatio,
            };

            // Set the property to animate
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Margin"));

            // Add animation to storyboard timeline
            storyboard.Children.Add(doubleAnimation);

        }
    }
}
