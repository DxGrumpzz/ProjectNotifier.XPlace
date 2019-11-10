namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Net.Http;
    using System.Security;
    using System.Threading.Tasks;

    /// <summary>
    /// An interface that specifes an implementation for a sign in manager
    /// </summary>
    public interface ISignInManager
    {

        /// <summary>
        /// Sign in using regular user credentials
        /// </summary>
        /// <param name="username"> The user's username </param>
        /// <param name="password"> The user's password </param>
        /// <param name="signSuccessfull"> An action that will be executed if the sign in was succesfull, contains the server reponse message </param>
        /// <param name="signInFailed"> An action that will be executed if the sign in was unsuccesfull, contains the server reponse message </param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SignInAsync(string username, SecureString password, Func<HttpResponseMessage, Task> signSuccessfull = null, Func<HttpResponseMessage, Task> signInFailed = null);


        /// <summary>
        /// Sign in using a stored cookie 
        /// </summary>
        /// <param name="cookie"> The stored cookie </param>
        /// <param name="signSuccessfull"> An action that will be executed if the sign in was succesfull, contains the server reponse message </param>
        /// <param name="signInFailed"> An action that will be executed if the sign in was unsuccesfull, contains the server reponse message </param>
        /// <returns></returns>
        public Task<HttpResponseMessage> CookieSignInAsync(string cookie, Func<HttpResponseMessage, Task> signSuccessfull = null, Func<HttpResponseMessage, Task> signInFailed = null);

    };
};
