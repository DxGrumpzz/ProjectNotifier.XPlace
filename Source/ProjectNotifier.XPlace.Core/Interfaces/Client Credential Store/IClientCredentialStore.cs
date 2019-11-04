namespace ProjectNotifier.XPlace.Core
{
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public interface IClientDataStore
    {

        public Task EnsureDataStoreCreatedAsync();

        public Task<LoginCredentialsDataModel> GetLoginCredentialsAsync();

        public Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials);
    };
};
