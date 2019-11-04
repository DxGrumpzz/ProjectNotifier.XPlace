namespace ProjectNotifier.XPlace.Relational
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// 
    /// </summary>
    public class ClientDataStore : IClientDataStore
    {

        private readonly ClientDataStoreDBContext _clientDataStoreDBContext;


        public ClientDataStore(ClientDataStoreDBContext clientDataStoreDBContext)
        {
            _clientDataStoreDBContext = clientDataStoreDBContext;

        }


        public async Task EnsureDataStoreCreatedAsync()
        {
            await _clientDataStoreDBContext.Database.EnsureCreatedAsync();
        }

        public async Task<LoginCredentialsDataModel> GetLoginCredentialsAsync()
        {
            return await Task.FromResult(_clientDataStoreDBContext.LoginCredentials.FirstOrDefault());
        }

        public async Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials)
        {
            _clientDataStoreDBContext.LoginCredentials.RemoveRange(_clientDataStoreDBContext.LoginCredentials);

            _clientDataStoreDBContext.LoginCredentials.Add(loginCredentials);

            await _clientDataStoreDBContext.SaveChangesAsync();
        }
    };
};
