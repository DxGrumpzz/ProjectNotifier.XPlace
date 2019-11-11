namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;


    public interface IResourceLocator
    {
        public ResourceInfo FindResource(string resourceName);

        public IEnumerable<ResourceInfo> FindResources(IEnumerable<string> resources);

    };
};