namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Net.Http;
    using System.Security;
    using System.Threading.Tasks;
    using ProjectNotifier.XPlace.Core;

    /// <summary>
    /// An implementation of the <see cref="ISignInManager"/>
    /// </summary>
    public class SignInManager : ISignInManager
    {
        private readonly IServerConnection _serverConnection;

        public SignInManager(IServerConnection serverConnection)
        {
            _serverConnection = serverConnection;
        }


        public Task<HttpResponseMessage> CookieSignInAsync(string cookie)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sign in using regular user credentials
        /// </summary>
        /// <param name="username"> The user's username </param>
        /// <param name="password"> The user's password </param>
        /// <param name="signSuccessfull"> An action that will be executed if the sign in was succesfull, contains the server reponse message </param>
        /// <param name="signInFailed"> An action that will be executed if the sign in was unsuccesfull, contains the server reponse message </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SignInAsync(string username, SecureString password, Action<HttpResponseMessage> signSuccessfull, Action<HttpResponseMessage> signInFailed)
        {
            // Attemp sign in
            var response = await _serverConnection.Client.PostAsJsonAsync("https://localhost:5001/Account/Login",
                new LoginRequestModel()
                {
                    Username = username,
                    Password = password.Unsecure(),
                });

            // If sign in has failed
            if (response.IsSuccessStatusCode == false)
            {
                // Call sign in failed action
                signInFailed?.Invoke(response);
            }
            // If sign in was succesfull
            else
            {
                // Call sign in succeded action
                signSuccessfull?.Invoke(response);
            };

            return response;
        }
    };
};