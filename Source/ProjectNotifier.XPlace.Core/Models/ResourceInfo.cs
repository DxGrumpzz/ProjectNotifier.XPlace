namespace ProjectNotifier.XPlace.Core
{
    using System.IO;

    /// <summary>
    /// A class that contains information about an embedded resource
    /// </summary>
    public class ResourceInfo
    {

        #region Public properties

        /// <summary>
        /// The name of the resource
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// The resource's data stream
        /// </summary>
        public Stream ResourceStream { get; set; }

        /// <summary>
        /// The resource fullname/path
        /// </summary>
        public string ResourceFullname { get; set; }

        #endregion


        #region Public methods

        /// <summary>
        /// Returns the Resource's data Stream to a byte array
        /// </summary>
        /// <returns></returns>
        public byte[] ToByteArray()
        {
            // Validation
            if (ResourceStream is null || ResourceStream.Length == 0)
                return null;
            
            // Holds the Resource's data stream 
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Copy resource stream data to the memory stream
                ResourceStream.CopyTo(memoryStream);

                return memoryStream.ToArray();
            };
        }

        #endregion
    };
};