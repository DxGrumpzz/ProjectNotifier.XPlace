namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface that specifies a definition for an app's resource storage
    /// </summary>
    public interface IAppResourceStore
    {

        /// <summary>
        /// Stores a single resource
        /// </summary>
        /// <param name="resourceName"> The name of the resource </param>
        public void AddResource(string resourceName);

        /// <summary>
        /// Stores multiple resources 
        /// </summary>
        /// <param name="resources"> The namees of the resources </param>
        public void AddResources(IEnumerable<string> resources);


        public void AddResourcesFromFolder(string folderName);

        /// <summary>
        /// Fetches a single resource from the resource store
        /// </summary>
        /// <param name="resourceName"> The names of the resource </param>
        /// <returns></returns>
        public ResourceInfo GetResource(string resourceName);

        /// <summary>
        /// Fetches multiple resources
        /// </summary>
        /// <param name="resources"> The names of the resources to retrieve </param>
        /// <returns></returns>
        public IEnumerable<ResourceInfo> GetResources(IEnumerable<string> resources);

    };
};