namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;


    /// <summary>
    ///
    /// </summary>
    public class ProjectPreferenceSelectionMenuViewModel : BaseViewModel
    {

        public static ProjectPreferenceSelectionMenuViewModel DesignInstance => new ProjectPreferenceSelectionMenuViewModel(null)
        {
            IsMenuOpen = true,
        };


        #region Private fields

        private bool _isMenuOpen;

        #endregion


        #region Public properties

        /// <summary>
        /// The list of project types that the user can select from 
        /// </summary>
        public List<ProjectTypes> AvailableProjectTypes { get; set; }

        /// <summary>
        /// A boolean flag that indicates if the menu is open
        /// </summary>
        public bool IsMenuOpen
        {
            get => _isMenuOpen;
            set
            {
                _isMenuOpen = value;
                OnPropertyChanged();
            }
        }

        #endregion


        #region Commands

        public ICommand CloseMenuCommand { get; }

        #endregion


        /// <summary>
        /// Default constructor, Takes a list of the user's already selected/prefered project types
        /// </summary>
        /// <param name="userProjects"> The user's prefered project types </param>
        public ProjectPreferenceSelectionMenuViewModel(IEnumerable<ProjectTypes> userProjects)
        {
            AvailableProjectTypes = new List<ProjectTypes>(userProjects);



            CloseMenuCommand = new RelayCommand(ExecuteCloseMenuCommand);
        }


        private void ExecuteCloseMenuCommand()
        {
            IsMenuOpen = false;
        }
    };
};
