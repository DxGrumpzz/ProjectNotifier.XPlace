namespace ProjectNotifier.XPlace.Core
{
    using Microsoft.AspNetCore.Identity;
    using System.Collections.Generic;

    /// <summary>
    /// A model class for the app's user/client
    /// </summary>
    public class AppUserModel : IdentityUser
    {

        /// <summary>
        /// The user's project preferences
        /// </summary>
        public ICollection<UserProjectPreference> UserProjectPreferences { get; set; }

    };
};
