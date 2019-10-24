namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A model class that contains the app's settings
    /// </summary>
    public class ClientAppSettingsModel
    {
   
        #region Private properties

        private readonly IConfig _config;

        private int _projectsToDisplay;
        private int _keepNotificationOpenSeconds;
        private bool _rememberMe;

        #endregion

     
        /// <summary>
        /// How many projects to read/display
        /// </summary>
        public int ProjectsToDisplay
        {
            get => _projectsToDisplay;
            set
            {
                _projectsToDisplay = value;
                _config?.SetValue(nameof(ProjectsToDisplay), value);
            }
        }

        /// <summary>
        /// How long to display the new projects notification in seconds
        /// </summary>
        public int KeepNotificationOpenSeconds
        {
            get => _keepNotificationOpenSeconds;
            set
            {
                _keepNotificationOpenSeconds = value;
                _config?.SetValue(nameof(ProjectsToDisplay), value);
            }
        }

        /// <summary>
        /// Remember user credentials and auto login when app starts
        /// </summary>
        public bool RememberMe
        {
            get => _rememberMe;
            set
            {
                _rememberMe = value;
                _config?.SetValue(nameof(RememberMe), value);
            }
        }


        public ClientAppSettingsModel(IConfig config = null)
        {
            _config = config;

            // Initliaze memebers if config isn't null
            if(config != null)
            {
                _projectsToDisplay = config.GetValue<int>(nameof(ProjectsToDisplay));
                _keepNotificationOpenSeconds = config.GetValue<int>(nameof(KeepNotificationOpenSeconds));
                _rememberMe = config.GetValue<bool>(nameof(RememberMe));
            };
        }
    };
}
