namespace ProjectNotifier.XPlace.Client
{
    using System.Windows;

    /// <summary>
    /// 
    /// </summary>
    public class AnimationBaseAttachedProperty<TParent> : BaseAttachedProperty<TParent, bool>
        where TParent : BaseAttachedProperty<TParent, bool>, new()
    {

        #region Public Properties

        /// <summary>
        /// A flag indicating if this is the first time this property has been loaded
        /// </summary>
        public bool FirstLoad { get; set; } = true;

        #endregion

        public override void OnValueUpdated(DependencyObject sender, object value)
        {
            // Get the framework element
            if (!(sender is FrameworkElement element))
                return;

            // Don't fire if the value doesn't change
            if (sender.GetValue(ValueProperty) == value && !FirstLoad)
                return;

            // On first load...
            if (FirstLoad)
            {
                // Create a single self-unhookable event 
                // for the elements Loaded event
                RoutedEventHandler onLoaded = null;
                onLoaded = (ss, ee) =>
                {
                    // Unhook ourselves
                    element.Loaded -= onLoaded;

                    // Do desired animation
                    DoAnimation(element, (bool)value);

                    // No longer in first load
                    FirstLoad = false;
                };

                // Hook into the Loaded event of the element
                element.Loaded += onLoaded;
            }
            else
                // Do desired animation
                DoAnimation(element, (bool)value);
        }


        /// <summary>
        /// The animation method that is fired when the value changes
        /// </summary>
        /// <param name="element">The element</param>
        /// <param name="value">The new value</param>
        protected virtual void DoAnimation(FrameworkElement element, bool value) { }
    };

    public class AnimateSlideOutToTopProperty : AnimationBaseAttachedProperty<AnimateSlideOutToTopProperty>
    {
        protected override async void DoAnimation(FrameworkElement element, bool value)
        {
            if (value == true)
                await element.SlideOutToTopAsync(0.5f);
            else
                await element.SlideInFromTopAsync(0.5f);
        }
    }
};
