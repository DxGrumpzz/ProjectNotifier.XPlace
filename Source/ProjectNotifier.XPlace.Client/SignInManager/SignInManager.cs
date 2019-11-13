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


        /// <summary>
        /// Sign in using a stored cookie 
        /// </summary>
        /// <param name="cookie"> The stored cookie </param>
        /// <param name="signSuccessfull"> An action that will be executed if the sign in was succesfull, contains the server reponse message </param>
        /// <param name="signInFailed"> An action that will be executed if the sign in was unsuccesfull, contains the server reponse message </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> CookieSignInAsync(string cookie, Func<HttpResponseMessage, Task> signSuccessfull, Func<HttpResponseMessage, Task> signInFailed)
        {
            // Set the cookies
            _serverConnection.Cookies.SetCookies(new Uri("https://localhost:5001"), cookie);

            // Get project list
            var response = await DI.GetService<IServerConnection>().Client.PostAsync($"https://localhost:5001/Account/Login", null);//$"https://localhost:5001/Projects");

            // If sign in has failed
            if (response.IsSuccessStatusCode == false)
            {
                // Call sign in failed action
                await signInFailed?.Invoke(response);
            }
            // If sign in was succesfull
            else
            {
                // Call sign in succeded action
                await signSuccessfull?.Invoke(response);
            };

            return response;
        }


        /// <summary>
        /// Sign in using regular user credentials
        /// </summary>
        /// <param name="username"> The user's username </param>
        /// <param name="password"> The user's password </param>
        /// <param name="signSuccessfull"> An action that will be executed if the sign in was succesfull, contains the server reponse message </param>
        /// <param name="signInFailed"> An action that will be executed if the sign in was unsuccesfull, contains the server reponse message </param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> SignInAsync(string username, SecureString password, Func<HttpResponseMessage, Task> signSuccessfull, Func<HttpResponseMessage, Task> signInFailed)
        {
            // Attemp sign in
            var response = await _serverConnection.Client.PostAsJsonAsync("https://localhost:5001/Account/Login/{LoginModel}",
                new LoginRequestModel()
                {
                    Username = username,
                    Password = password.Unsecure(),
                });

            // If sign in has failed
            if (response.IsSuccessStatusCode == false)
            {
                // Call sign in failed action
                await signInFailed?.Invoke(response);
            }
            // If sign in was succesfull
            else
            {
                // Call sign in succeded action
                await signSuccessfull?.Invoke(response);
            };

            return response;
        }
    };
};