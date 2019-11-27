namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;


    /// <summary>
    ///
    /// </summary>
    public class ProjectPreferenceSelectionMenuViewModel : BaseViewModel
    {
        public static ProjectPreferenceSelectionMenuViewModel DesignInstance => new ProjectPreferenceSelectionMenuViewModel(
            new List<ProjectType>()
            {
                ProjectType.Administration,
                ProjectType.Translations,
                ProjectType.WritingAndEditing,
                ProjectType.SAP,
            })
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
        public ObservableCollection<ProjectPreferenceMenuItemViewModel> AvailableProjectType { get; set; }

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
        public ProjectPreferenceSelectionMenuViewModel(IEnumerable<ProjectType> userProjects)
        {
            // Load available projects
            AvailableProjectType = new ObservableCollection<ProjectPreferenceMenuItemViewModel>(GetNeccessaryProjects(userProjects));


            CloseMenuCommand = new RelayCommand(ExecuteCloseMenuCommand);
        }


        #region Command callback

        private void ExecuteCloseMenuCommand()
        {
            IsMenuOpen = false;
        }

        #endregion

        #region Private helpers

        /// <summary>
        /// Retrieves a list of preffered project types depending on the user's already existing list
        /// </summary>
        /// <param name="userProjects"> The list of the user's preffered projects </param>
        private IEnumerable<ProjectPreferenceMenuItemViewModel> GetNeccessaryProjects(IEnumerable<ProjectType> userProjects)
        {
            // Get a array of integers representing each value in ProjectType
            var projectTypeNumericValues = ((int[])Enum.GetValues(typeof(ProjectType)))
                .Select(projectTypeValue => (ProjectType)projectTypeValue);

            // Get only the project types that aren't in the user's projects list
            var differences = projectTypeNumericValues.Except(userProjects)
            // Convert list of ProjectType to ProjectPreferenceMenuItemViewModel
            .Select(projectType => new ProjectPreferenceMenuItemViewModel()
            {
                ProjectType = projectType,
            })
            .OrderBy(project => project.ProjectType.ToString());

            return differences;
        }

        #endregion
    };
};
