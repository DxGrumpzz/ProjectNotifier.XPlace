namespace ProjectNotifier.XPlace.Core
{
    using System.Threading.Tasks;

    /// <summary>
    /// An easy acecss to local client data storage
    /// </summary>
    public interface IClientDataStore
    {

        /// <summary>
        /// Ensures the local database was created
        /// </summary>
        /// <returns></returns>
        public Task EnsureDataStoreCreatedAsync();

        /// <summary>
        /// Retreives the user's saved credentials
        /// </summary>
        /// <returns></returns>
        public Task<LoginCredentialsDataModel> GetLoginCredentialsAsync();

        /// <summary>
        /// Saves the user's login credentials locally
        /// </summary>
        /// <param name="loginCredentials"> The credentials to save </param>
        /// <returns></returns>
        public Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials);
    };
};
