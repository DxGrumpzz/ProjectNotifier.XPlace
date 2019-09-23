namespace ProjectNotifier.XPlace.Client
{

	using ProjectNotifier.XPlace.Core;
	using System.Collections.Generic;
	using System.Threading.Tasks;


	/// <summary>
	/// Specifies an implementation of a project loader/reader
	/// </summary>
	public interface IProjectLoader
	{

		/// <summary>
		/// Returns a <see cref="ProjectListViewModel"/> containing a list of projects
		/// </summary>
		/// <returns></returns>
		public Task<IEnumerable<ProjectModel>> LoadProjectsAsync();


	}
}