namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;
    using System.Windows.Controls;

    /// <summary>
    /// 
    /// </summary>
    public static class FirstViewExtensions
    {

        /// <summary>
        /// Converts a <see cref="FirstViewViews"/> to a <see cref="UserControl"/> with a DataContex as the <paramref name="viewModel"/>
        /// </summary>
        /// <param name="view"> The type of view </param>
        /// <param name="viewModel"> The view's datacontex/viewmodel </param>
        /// <returns></returns>
        public static UserControl ToFirstView(this FirstViewViews view, object viewModel = null)
        {
            switch(view)
            {
                case FirstViewViews.Login:
                {
                    return new LoginView((LoginViewModel)viewModel);
                };

                case FirstViewViews.Register:
                {
                    return new RegisterView((RegisterViewModel)viewModel);
                };

                // This shouldn't happen
                default:
                {
                    Debugger.Break();
                    return null;
                };
            }
        }
    };
};
