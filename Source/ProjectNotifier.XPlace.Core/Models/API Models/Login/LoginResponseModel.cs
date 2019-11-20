namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class LoginResponseModel
    {

        public UserProfileModel UserProfile { get; set; }

        public IEnumerable<ProjectModel> Projects { get; set; }

    };
};
