﻿namespace ProjectNotifier.XPlace.Core
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface that specifices client side caches
    /// </summary>
    public interface IClientCache
    {

        /// <summary>
        /// A list that holds the latest projects available
        /// </summary>
        public IEnumerable<ProjectModel> ProjectListCache { get; set; }

        /// <summary>
        /// A list that holds the user's preffered projects
        /// </summary>
        public IEnumerable<ProjectModel> UserPrefferedProjectsCache { get; }

    };
}
