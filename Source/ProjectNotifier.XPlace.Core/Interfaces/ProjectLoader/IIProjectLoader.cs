namespace ProjectNotifier.XPlace.Client
{

	using ProjectNotifier.XPlace.Core;
	using System;
	using System.Collections.Generic;


	/// <summary>
	/// Specifies an implementation of a project loader/reader
	/// </summary>
	public interface IIProjectLoader
	{

		/// <summary>
		/// An event that will be invkoed when the ProjectLoader has loaded a new list of projects
		/// </summary>
		public event Action<ProjectModel> ProjectsListUpdated;


		/// <summary>
		/// Returns a <see cref="ProjectListViewModel"/> containing a list of projects
		/// </summary>
		/// <returns></returns>
		public IEnumerable<ProjectModel> LoadProjects();

	}
}