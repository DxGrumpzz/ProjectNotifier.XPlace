namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A static class containing routes to API controllers and actions
    /// </summary>
    public static class ApiRoutes
    {

        /// <summary>
        /// The web server's url 
        /// </summary>
        public const string WEB_SERVER = "Https://LocalHost:5001";
        

        #region Account Controller

        /// <summary>
        /// An api route for the account contoller
        /// </summary>
        public const string API_ACCOUNT_CONTROLLER = "Account";

        /// <summary>
        /// An api route for the Login action in account contoller
        /// </summary>
        public const string API_ACCOUNT_CONTROLLER_LOGIN = "Login";

        /// <summary>
        /// An api route for the register action in account contoller
        /// </summary>
        public const string API_ACCOUNT_CONTROLLER_REGISTER = "Register";

        #endregion


        #region Profile Controller

        /// <summary>
        /// An api route for the profile contoller
        /// </summary>
        public const string API_PROFILE_CONTROLLER = "Profile";

        /// <summary>
        /// An api route for the UpdateUserPreferences Action in the profile contoller
        /// </summary>
        public const string API_PROFILE_UPDATE_USER_PREFERENCES = "UpdateUserPreferences";

        #endregion

    };
};