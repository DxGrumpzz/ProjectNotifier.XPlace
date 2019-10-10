namespace ProjectNotifier.XPlace.Client
{
    using System.Security;

    /// <summary>
    /// An interfaaace that specifes that a view has multiple password fields (password, and confirm password)
    /// </summary>
    public interface IHaveMultiplePassword
    {
        public SecureString Password { get; }
        public SecureString ConfirmPassword { get; }
    };
}
