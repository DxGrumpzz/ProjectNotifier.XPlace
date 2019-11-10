namespace ProjectNotifier.XPlace.Core
{
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
        /// <param name="password"> The user's password  </param>
        /// <returns></returns>
        public Task<HttpResponseMessage> SignInAsync(string username, SecureString password);


        /// <summary>
        /// Sign in using a stored cookie 
        /// </summary>
        /// <param name="cookie"> The stored cookie </param>
        /// <returns></returns>
        public Task<HttpResponseMessage> CookieSignInAsync(string cookie);

    };
};
