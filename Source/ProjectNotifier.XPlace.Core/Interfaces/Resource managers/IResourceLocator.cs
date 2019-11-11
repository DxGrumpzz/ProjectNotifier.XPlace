namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface that specifies an implementation of resource locator for an app's embedded resources
    /// </summary>
    public interface IResourceLocator
    {
        /// <summary>
        /// Finds a single resource by name without extension (without .exe, .txt, .svg, etc...)
        /// </summary>
        /// <param name="resourceName"> The name of the resource without extension </param>
        /// <returns></returns>
        public ResourceInfo FindResource(string resourceName);

        /// <summary>
        /// Finds multiple resources by name
        /// </summary>
        /// <param name="resources"> The names of the resources </param>
        /// <returns></returns>
        public IEnumerable<ResourceInfo> FindResources(IEnumerable<string> resources);

    };
};