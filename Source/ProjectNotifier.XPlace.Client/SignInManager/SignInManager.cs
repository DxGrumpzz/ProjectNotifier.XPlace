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
            // Get project list
            var response = await _serverConnection.CookieLoginAsync(cookie);


            // If sign in has failed
            if (response.IsSuccessStatusCode == false)
            {
                // Call sign in failed action
                await signInFailed?.Invoke(response);
             
                DI.Logger().Log($"Sign-in failed, Server error response {response.StatusCode}/{(int)response.StatusCode}", LogLevel.Verbose);
            }
            // If sign in was succesfull
            else
            {
                await LoginSuccess(response);

                // Call sign in succeded action
                await signSuccessfull?.Invoke(response);
                DI.Logger().Log("Succesfully logged in", LogLevel.Informative);
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
            var response = await _serverConnection.LoginAsync(username, password);

            // If sign in has failed
            if (response.IsSuccessStatusCode == false)
            {
                // Call sign in failed action
                await signInFailed?.Invoke(response);
             
                DI.Logger().Log($"Sign-in failed, Server error response {response.StatusCode}/{(int)response.StatusCode}", LogLevel.Verbose);
            }
            // If sign in was succesfull
            else
            {
                await LoginSuccess(response);

                // Call sign in succeded action
                await signSuccessfull?.Invoke(response);
              
                DI.Logger().Log("Succesfully logged in", LogLevel.Informative);
            };

            return response;
        }


        private async Task LoginSuccess(HttpResponseMessage response)
        {
            // Convert response to a list of projects
            var responseContent = await response.Content.ReadAsAsync<LoginResponseModel>();

            // Save cookie
            await DI.GetService<IClientDataStore>().SaveLoginCredentialsAsync(
                new LoginCredentialsDataModel()
                {
                    Cookie = _serverConnection.CookieContainer.GetCookieHeader(new Uri(ApiRoutes.API_URL))
                });


            // Build hub connection
            await _serverConnection.StartHubConnectionAsync("Https://LocalHost:5001/ProjectsHub", _serverConnection.CookieContainer);


            // Update cache
            DI.GetService<IClientCache>().ProjectListCache = responseContent.Projects;

            // Save profile 
            DI.GetService<IClientDataStore>().SaveUserProfile(responseContent.UserProfile);


            // Display the user's project preferences
            DI.GetService<ProjectsPageViewModel>().UpdateProjectsList();
        }
    };
};