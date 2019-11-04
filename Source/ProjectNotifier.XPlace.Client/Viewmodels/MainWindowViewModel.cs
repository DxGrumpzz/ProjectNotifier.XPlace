namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;

    public class MainWindowViewModel : BaseViewModel
    {

        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MainWindowModel _model;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BaseView _currentPage;

        #endregion


        #region Public properties

        /// <summary>
        /// A model associated with this view
        /// </summary>
        public MainWindowModel Model
        {
            get => _model;
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// The currently displayed view
        /// </summary>
        public BaseView CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }


        #endregion

        public MainWindowViewModel()
        {
            CurrentPage = new LoginView()
            {
                ViewModel = new LoginViewModel(),
            };

        }
    };
};