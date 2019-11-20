namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A model class that represetnts a user's profile data
    /// </summary>
    public class UserProfileModel
    {
        /// <summary>
        /// The user's username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user's unique ID
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// The user's project preferences list
        /// </summary>
        public IEnumerable<ProjectTypes> UserProjectPreferences { get; set; }
    };
};
