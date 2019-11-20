namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class LoginResponseModel
    {
        /// <summary>
        /// The user's profile information
        /// </summary>
        public UserProfileModel UserProfile { get; set; }

        public IEnumerable<ProjectModel> Projects { get; set; }

    };
};
