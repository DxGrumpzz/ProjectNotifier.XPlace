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




        /// <summary>
        /// Retreives the user's saved application settings
        /// </summary>
        /// <returns></returns>
        public Task<AppSettingsDataModel> GetClientAppSettingsAsync();

        /// <summary>
        /// Retreives the user's saved application settings asynchronously
        /// </summary>
        /// <returns></returns>
        public AppSettingsDataModel GetClientAppSettings();


        /// <summary>
        /// Saves the user's login appliation settings locally
        /// </summary>
        /// <param name="settings"> The settings to save </param>
        /// <returns></returns>
        public Task SaveClientAppSettingsAsync();

        /// <summary>
        /// Saves the user's login appliation settings locally
        /// </summary>
        /// <param name="settings"> The settings to save </param>
        /// <returns></returns>
        public void SaveClientAppSettings();


    };
};
