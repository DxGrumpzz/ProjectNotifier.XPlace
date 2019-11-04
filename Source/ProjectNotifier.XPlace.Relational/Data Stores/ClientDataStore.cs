namespace ProjectNotifier.XPlace.Relational
{
    using System.Linq;
    using System.Threading.Tasks;

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
            await _clientDataStoreDBContext.Database.EnsureCreatedAsync();
        }

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
    };
};
