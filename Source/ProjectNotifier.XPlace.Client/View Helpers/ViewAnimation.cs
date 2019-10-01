namespace ProjectNotifier.XPlace.Client
{

    /// <summary>
    /// The starting animtion for when a pge loads
    /// </summary>
    public enum ViewAnimation
    {
        /// <summary>
        /// No animation takes place
        /// </summary>
        None = 0,

        /// <summary>
        /// Slides a page from the top into view
        /// </summary>
        SlideInFromTop = 1,

        /// <summary>
        /// Slides a page upwards
        /// </summary>
        SlideOutToTop = 2,

        /// <summary>
        /// Slides a page to the bottom
        /// </summary>
        SlideOutToBottom = 3,
    }
}