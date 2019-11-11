namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;


    public interface IAppResourceStore
    {

        public void AddResource(string resourceName);

        public void AddResources(IEnumerable<string> resources);

        public void AddResourcesFromFolder(string folderName);

        public ResourceInfo GetResource(string resourceName);

        public IEnumerable<ResourceInfo> GetResources(IEnumerable<string> resources);

    };
};