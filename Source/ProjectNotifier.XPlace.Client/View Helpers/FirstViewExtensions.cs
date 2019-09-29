namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;
    using System.Windows.Controls;

    /// <summary>
    /// 
    /// </summary>
    public static class MainPageExtensions
    {

        /// <summary>
        /// Converts a <see cref="MainPageViews"/> to a <see cref="UserControl"/> with a DataContex as the <paramref name="viewModel"/>
        /// </summary>
        /// <param name="view"> The type of view </param>
        /// <param name="viewModel"> The view's datacontex/viewmodel </param>
        /// <returns></returns>
        public static UserControl ToMainPage(this MainPageViews view, object viewModel = null)
        {
            switch(view)
            {
                case MainPageViews.Login:
                {
                    return new LoginView((LoginViewModel)viewModel);
                };

                case MainPageViews.Register:
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
