namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// 
    /// </summary>
    public class LoginResponseModel
    {
        public AppUserModel UserModel { get; set; }


        public string Username { get; set; }

        public string UserID { get; set; }

        public IEnumerable<ProjectTypes> UserProjectPreferences { get; set; }


        public IEnumerable<ProjectModel> Projects { get; set; }

    };
};
