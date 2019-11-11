namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// A resource locator that finds embedded resources in an application
    /// </summary>
    public class ResourceLocator : IResourceLocator
    {

        #region Private fields

        /// <summary>
        /// The current assemlby, Where the resources are located
        /// </summary>
        private Assembly _currentAssembly;

        /// <summary>
        /// Names of resources in the current assembly
        /// </summary>
        private IEnumerable<string> _resourceNames;

        #endregion


        /// <summary>
        /// Default constructor. Creates a default assembly from the currently executing code
        /// </summary>
        public ResourceLocator() :
            this(Assembly.GetExecutingAssembly())
        {
        }

        public ResourceLocator(Assembly assembly)
        {
            _currentAssembly = assembly;
            _resourceNames = assembly.GetManifestResourceNames();
        }



        #region Public functions

        /// <summary>
        /// Finds an embedded resource by name. Returns the first match it finds
        /// </summary>
        /// <param name="resourceName"> The name of the resource </param>
        /// <returns></returns>
        public ResourceInfo FindResource(string resourceName)
        {
            var foundResource = _resourceNames.
                // Finds the first match, Return null or default if no match was found
                FirstOrDefault(_resourceName => _resourceName.Contains(resourceName));

            // Check if resource exists
            if (foundResource is null)
                // Exit if it doesn't
                return null;

            // Gets the resource's data stream
            Stream resourceStream = _currentAssembly.GetManifestResourceStream(foundResource);


            return new ResourceInfo()
            {
                ResourceName = resourceName,
                ResourceStream = resourceStream,
                ResourceFullname = FormatResourceFullname(foundResource),
            };
        }


        /// <summary>
        /// Finds embedded resources by name. Returns a list of matching results
        /// </summary>
        /// <param name="resources"> The name of the resource </param>
        /// <returns> Returns an <see cref="IEnumerable{T}"/> that contains ResourceInfo </returns>
        public IEnumerable<ResourceInfo> FindResources(IEnumerable<string> resources)
        {
            // A list that holds the found resources
            List<ResourceInfo> resourceList = new List<ResourceInfo>();

            // Trun resources into a List<>
            resources.ToList()
                // Iterate through the items
                .ForEach(resourceName =>
                {
                    // Try find the resource 
                    var resource = FindResource(resourceName);

                    // If resource exists
                    if (resource != null)
                        // Add to list
                        resourceList.Add(resource);
                });


            return resourceList;
        }

        #endregion

        #region Private functions

        /// <summary>
        /// Returns a new string containing a formatted resource name instead of '.' it will change to '\'
        /// </summary>
        /// <param name="resourceFullname"> </param>
        /// <returns></returns>
        private string FormatResourceFullname(string resourceFullname)
        {
            StringBuilder stringBuilder = new StringBuilder(resourceFullname);

            // The name of the current project
            var projectName = _currentAssembly.GetName().Name;

            // Replace characters
            stringBuilder.Remove(0, projectName.Length + 1);

            // Get the last index of the '.' character
            int index = stringBuilder.ToString().LastIndexOf('.');

            // Repalce every '.' character with a '//' except for the last one
            stringBuilder.Replace('.', '\\', 0, index);

            return stringBuilder.ToString();
        }

        #endregion
    };
};