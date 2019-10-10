namespace ProjectNotifier.XPlace.Client
{
    using System.Security;

    /// <summary>
    /// An interface that specifes that a view has a password
    /// </summary>
    public interface IHavePassword
    {
        public SecureString Password { get; }
    };
}
