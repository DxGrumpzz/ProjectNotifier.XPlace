namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A data models that holds login/credential information
    /// </summary>
    public class LoginCredentialsDataModel
    {
        /// <summary>
        /// The row's ID in the local database
        /// </summary>
        public string DataModelID { get; set; }

        /// <summary>
        /// The cookie that allows authorization and automatic authentication
        /// </summary>
        public string Cookie { get; set; }

    };
};
