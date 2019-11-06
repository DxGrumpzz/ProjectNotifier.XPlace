namespace ProjectNotifier.XPlace.Relational
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// The implementation for <see cref="IClientDataStore"/>
    /// </summary>
    public class ClientDataStore : IClientDataStore
    {

        /// <summary>
        /// The client's local database storage
        /// </summary>
        private readonly ClientDataStoreDBContext _clientDataStoreDBContext;

        private readonly object _synchronizingObject = new object();

        public ClientDataStore(ClientDataStoreDBContext clientDataStoreDBContext)
        {
            _clientDataStoreDBContext = clientDataStoreDBContext;
        }


        /// <summary>
        /// Ensures the local database was created
        /// </summary>
        /// <returns></returns>
        public async Task EnsureDataStoreCreatedAsync()
        {
            // Call entity framework Ensure craeted to ensure database creation
            if (await _clientDataStoreDBContext.Database.EnsureCreatedAsync() == true)
            {
                _clientDataStoreDBContext.ClientAppSettings.Add(new AppSettingsDataModel()
                {
                    KeepNotificationOpenSeconds = 5,
                    ProjectsToDisplay = 25,
                    RememberMe = false,
                });

                await _clientDataStoreDBContext.SaveChangesAsync();
            }
            else
            {

            }
        }


        #region Login credentials data store

        /// <summary>
        /// Retreives the user's saved credentials
        /// </summary>
        /// <returns></returns>
        public async Task<LoginCredentialsDataModel> GetLoginCredentialsAsync()
        {
            // Awaits a Task result that takes the Login credentials from the local database
            return await Task.FromResult(_clientDataStoreDBContext.LoginCredentials.FirstOrDefault());
        }


        /// <summary>
        /// Saves the user's login credentials locally
        /// </summary>
        /// <param name="loginCredentials"> The credentials to save </param>
        /// <returns></returns>
        public async Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials)
        {
            // To ensure no duplicates and other unexpected behaviours 
            // Delete all Login credentials
            _clientDataStoreDBContext.LoginCredentials.RemoveRange(_clientDataStoreDBContext.LoginCredentials);

            // Add the new credentials
            _clientDataStoreDBContext.LoginCredentials.Add(loginCredentials);

            // Save changes
            await _clientDataStoreDBContext.SaveChangesAsync();
        }

        #endregion


        #region Client app settings data store

        /// <summary>
        /// Retreives the user's saved application settings asynchronously
        /// </summary>
        /// <returns></returns>
        public async Task<AppSettingsDataModel> GetClientAppSettingsAsync()
        {
            // Find settings in local DB and return the first result
            return await Task.FromResult(GetClientAppSettings());
        }

        /// <summary>
        /// Retreives the user's saved application settings
        /// </summary>
        /// <returns></returns>
        public AppSettingsDataModel GetClientAppSettings()
        {
            lock (_synchronizingObject)
            {
                // Find settings in local DB and return the first result
                return _clientDataStoreDBContext.ClientAppSettings.FirstOrDefault();
            };
        }

        /// <summary>
        /// Saves the user's login appliation settings locally
        /// </summary>
        /// <param name="settings"> The settings to save </param>
        /// <returns></returns>
        public async Task SaveClientAppSettingsAsync()
        {
            await Task.Run(() => SaveClientAppSettings());
        }


        /// <summary>
        /// Saves the user's login appliation settings locally
        /// </summary>
        /// <returns></returns>
        public void SaveClientAppSettings()
        {
            lock (_synchronizingObject)
            {
                // Save changes
                _clientDataStoreDBContext.SaveChanges();
            };
        }


        #endregion

    };
};