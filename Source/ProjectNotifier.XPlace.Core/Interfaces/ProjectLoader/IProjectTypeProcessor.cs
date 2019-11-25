namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface for a processor of text that is used to determine the type of a project, Whether it is development, desgin, and others...
    /// </summary>
    public interface IProjectTypeProcessor
    {
        
        /// <summary>
        /// Takes a <see cref="ProjectModel"/> and finds it's fitting types
        /// </summary>
        /// <param name="project"> The project to check </param>
        /// <returns> Returns an <see cref="IEnumerable{T}"/> of <see cref="ProjectType"/> </returns>
        public IEnumerable<ProjectType> GetProjectType(in ProjectModel project);

    };
};