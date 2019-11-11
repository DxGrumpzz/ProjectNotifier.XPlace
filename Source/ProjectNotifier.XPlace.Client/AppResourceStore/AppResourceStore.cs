namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;


    /// <summary>
    /// The application's resource storage
    /// </summary>
    public class AppResourceStore : IAppResourceStore
    {
    
        #region Private fields

        /// <summary>
        /// A resource locator
        /// </summary>
        private IResourceLocator _resourceLocator;

        /// <summary>
        /// A dictionary that holds a <see cref="string"/> as key and a <see cref="ResourceInfo"/> as value
        /// </summary>
        private Dictionary<string, ResourceInfo> _resources;

        #endregion


        public AppResourceStore(IResourceLocator resourceLocator)
        {
            _resourceLocator = resourceLocator;

            _resources = new Dictionary<string, ResourceInfo>();
        }


        #region Public methods

        /// <summary>
        /// Stores a single resource
        /// </summary>
        /// <param name="resourceName"> The name of the resource </param>
        public void AddResource(string resourceName)
        {
            // Find the resource
            var resource = _resourceLocator.FindResource(resourceName);

            // If resource exists
            if (resource != null)
                // Add resource to list
                _resources.Add(resourceName, resource);
        }


        /// <summary>
        /// Stores multiple resources 
        /// </summary>
        /// <param name="resources"> The namees of the resources </param>
        public void AddResources(IEnumerable<string> resources)
        {
            // Find resources
            var appResources = _resourceLocator.FindResources(resources);

            // Turn appResources into a List<>
            appResources.ToList()
                // For every found resource...
                .ForEach(resource =>
                {
                    // Add it to the list
                    _resources.Add(resource.ResourceName, resource);
                });
        }

        public void AddResourcesFromFolder(string folderName)
        {
            // TODO: Add implementation maybe(?) 

            throw new NotImplementedException();
        }


        /// <summary>
        /// Fetches a single resource from the resource store
        /// </summary>
        /// <param name="resourceName"> The names of the resource </param>
        /// <returns></returns>
        public ResourceInfo GetResource(string resourceName)
        {
            // Finds checks if resource exists in the list
            _resources.TryGetValue(resourceName, out ResourceInfo resource);

            // If resource exists
            if (resource != null)
                // Return the resource
                return resource;
            else
                return null;
        }

        /// <summary>
        /// Fetches multiple resources
        /// </summary>
        /// <param name="resources"> The names of the resources to retrieve </param>
        /// <returns></returns>
        public IEnumerable<ResourceInfo> GetResources(IEnumerable<string> resources)
        {
            // A list that will hold the resources
            var foundResources = new List<ResourceInfo>();

            // Turn resources to a List<>
            resources.ToList()
            // For every requested resource
            .ForEach(resourceName =>
            {
                // Check if resource exists
                _resources.TryGetValue(resourceName, out ResourceInfo resource);

                // If resource exists
                if (resource != null)
                    // Add resource to list
                    foundResources.Add(resource);
            });

            return foundResources;
        } 

        #endregion

    };
};