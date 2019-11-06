namespace ProjectNotifier.XPlace.Core
{

	using System.Collections.Generic;
    using System.Threading;
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
		public Task<IEnumerable<ProjectModel>> LoadProjectsAsync(int count = 100, CancellationToken cancellationToken = default);


    }
}