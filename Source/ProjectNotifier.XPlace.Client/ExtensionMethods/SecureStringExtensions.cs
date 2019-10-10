namespace ProjectNotifier.XPlace.Client
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    /// <summary>
    /// A helper class for <see cref="SecureString"/>
    /// </summary>
    public static class SecureStringExtensions
    {

        /// <summary>
        /// "Converts" a <see cref="SecureString"/> to a unicode string
        /// </summary>
        /// <param name="secureString"> The secure string to unsecure </param>
        /// <returns></returns>
        public static string Unsecure(this SecureString secureString)
        {
            // Holds an unmanaged string in memory
            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                // Copy contents of secure srting to unmanagedString 
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);

                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                // Free memory
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            };
        }
    };
};
