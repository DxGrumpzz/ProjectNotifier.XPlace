﻿namespace ProjectNotifier.XPlace.Core
{
    /// <summary>
    /// A class that holds a user's project preference(s), made specifically for EntityFramework database
    /// </summary>
    public class UserProjectPreference
    {

        /// <summary>
        /// A unique ID for this row
        /// </summary>
        public string RowID { get; set; }

        /// <summary>
        /// The <see cref="AppUserModel "/> associated with this entry
        /// </summary>
        public AppUserModel User { get; set; }
        
        /// <summary>
        /// The type of project the user prefers to work on
        /// </summary>
        public ProjectTypes ProjectType { get; set; }

    };
};