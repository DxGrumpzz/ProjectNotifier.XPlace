namespace ProjectNotifier.XPlace.Client
{
    using System;
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


        /// <summary>
        /// Adds a slide animation that slides a control to the top 
        /// </summary>
        /// <param name="storyboard"> The storyboard </param>
        /// <param name="seconds"> How long the animation will take </param>
        /// <param name="offset"> The starting point where the animation will begin from </param>
        /// <param name="decelerationRatio"></param>
        public static void AddSlideOutToTop(this Storyboard storyboard, float seconds, double offset, float decelerationRatio = 0.9f)
        {
            // The slide in animation 
            ThicknessAnimation doubleAnimation = new ThicknessAnimation()
            {
                Duration = TimeSpan.FromSeconds(seconds),

                From = new Thickness(0),
                To = new Thickness(0, -offset, 0, offset),

                DecelerationRatio = decelerationRatio,
            };

            // Set the property to animate
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Margin"));

            // Add animation to storyboard timeline
            storyboard.Children.Add(doubleAnimation);
        }



        /// <summary>
        /// Adds a fade in animation to a storyboard animation timeline
        /// </summary>
        /// <param name="storyboard"></param>
        /// <param name="seconds"> For how long will the animation take place </param>
        public static void AddFadeIn(this Storyboard storyboard, float seconds)
        {
            // The slide in animation 
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(seconds),

                From = 0.0,
                To = 1.0,
            };

            // Set the property to animate
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));

            // Add animation to storyboard timeline
            storyboard.Children.Add(doubleAnimation);
        }


        /// <summary>
        /// Adds a fade out animation to a storyboard animation timeline
        /// </summary>
        /// <param name="storyboard"></param>
        /// <param name="seconds"> For how long will the animation take place </param>
        public static void AddFadeOut(this Storyboard storyboard, float seconds)
        {
            // The slide in animation 
            DoubleAnimation doubleAnimation = new DoubleAnimation()
            {
                Duration = TimeSpan.FromSeconds(seconds),

                From = 1.0,
                To = 0.0,
            };

            // Set the property to animate
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity"));

            // Add animation to storyboard timeline
            storyboard.Children.Add(doubleAnimation);
        }
    };
};