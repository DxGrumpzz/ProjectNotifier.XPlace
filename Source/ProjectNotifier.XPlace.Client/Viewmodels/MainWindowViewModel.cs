namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Diagnostics;

    public class MainWindowViewModel : BaseViewModel
    {

        #region Private fields

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MainWindowModel _model;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private BaseView _currentPage;

        private readonly ClientAppSettingsModel _settings;

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
            _settings = DI.GetService<ClientAppSettingsModel>();

            CurrentPage = new LoginView()
            {
                ViewModel = new LoginViewModel(_settings),
            };
        }
    };
};