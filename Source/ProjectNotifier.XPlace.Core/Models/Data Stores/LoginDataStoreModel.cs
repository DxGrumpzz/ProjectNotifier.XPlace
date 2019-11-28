namespace ProjectNotifier.XPlace.Core
{
    using System;

    /// <summary>
    /// A data models that holds login/credential information
    /// </summary>
    public class LoginCredentialsDataModel
    {
        /// <summary>
        /// The row's ID in the local database
        /// </summary>
        public string DataModelID { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// The cookie that allows authorization and automatic authentication
        /// </summary>
        public string Cookie { get; set; }

    };
};
