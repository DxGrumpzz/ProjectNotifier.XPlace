namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// 
    /// </summary>
    public class AppSettingsDataModel
    {

        /// <summary>
        /// A unique ID string for this row
        /// </summary>
        public string RowID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// How many projects to read/display
        /// </summary>
        public int ProjectsToDisplay { get; set; }

        /// <summary>
        /// How long to display the new projects notification in seconds
        /// </summary>
        public int KeepNotificationOpenSeconds { get; set; }

        /// <summary>
        /// Remember user credentials and auto login when app starts
        /// </summary>
        public bool RememberMe { get; set; }


    };
};