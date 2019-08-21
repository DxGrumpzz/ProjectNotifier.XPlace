namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	/// <summary>
	/// A project loader
	/// </summary>
	public class ProjectLoader
	{

		#region Private fields

		/// <summary>
		/// A boolean flag that indicates if currently auto updating
		/// </summary>
		private bool _isAutoUpdating = false;

		/// <summary>
		/// A cancellation token used to cancel auto update request
		/// </summary>
		private CancellationTokenSource _autoUpdateCancellationToken = new CancellationTokenSource();

		#endregion


		public double Interval { get; private set; }

		public SettingsModel SettingsModel { get; }


		/// <summary>
		/// An event that will be invkoed when the ProjectLoader has loaded a new list of projects
		/// </summary>
		public event Action<ProjectListViewModel> ProjectsListUpdated = (projectList) => { };


		public ProjectLoader(double interval, SettingsModel settingsModel)
		{
			Interval = interval;
			SettingsModel = settingsModel;
		}

		~ProjectLoader()
		{
			System.Diagnostics.Debug.WriteLine("ProjectLoader dtor");
		}


		/// <summary>
		/// Returns a <see cref="ProjectListViewModel"/> containing a list of projects
		/// </summary>
		/// <returns></returns>
		public ProjectListViewModel LoadProjects()
		{
			// Read rss feed
			var projects = new RSSReader(AppLinks.RSSFeedUrl)
			// Grab however many results the user requested from the RSS feed
			.GetXElementNodeList(count: SettingsModel.ProjectsToDisplay)
			// "Convert" the xml data to a ProjectModel
			.Select(element =>
			{
				// Select required nodes
				var titleNode = element.Element("title").Value;
				var linkNode = element.Element("link").Value;
				var descriptionNode = element.Element("description").Value;
				var publishDateNode = element.Element("pubDate").Value;


				return new ProjectItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						// Replace unicode identifiers(?) with string literals
						Title = FormatString(titleNode),
						Description = FormatString(descriptionNode),

						Link = linkNode,

						// Convert the date time to israel standard time
						PublishingDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Parse(publishDateNode), "Israel Standard Time"),
					},
				};
			});


			return new ProjectListViewModel()
			{
				ProjectList = new ObservableCollection<ProjectItemViewModel>(projects),
			};
		}


		/// <summary>
		/// Load a list of project every Interval count, and invoke <see cref="ProjectsListUpdated"/> event
		/// </summary>
		public void StartAutoUpdating()
		{
			// Don't run if already updating
			if(_isAutoUpdating == true)
				return;

			// Set updating flag to true
			_isAutoUpdating = true;

			// If cancellation request occured 
			if(_autoUpdateCancellationToken.IsCancellationRequested == true)
				// Restart cncellation token
				_autoUpdateCancellationToken = new CancellationTokenSource();


			// Spin a background task
			Task.Run(async () =>
			{
				// While updating flag is true
				while(_isAutoUpdating == true)
				{
					// Wait for interval
					await Task.Delay((int)Interval);

					// Invoke event
					ProjectsListUpdated?.Invoke(LoadProjects());
				};
			}, _autoUpdateCancellationToken.Token);
		}

		/// <summary>
		/// Stops auto updating project list
		/// </summary>
		public void StopAutoUpdating()
		{
			_isAutoUpdating = false;
			_autoUpdateCancellationToken.Cancel();
		}


		#region Private helpers

		/// <summary>
		/// Replaced unicode strings with string literalls
		/// </summary>
		/// <param name="stringToFormat"></param>
		private string FormatString(string stringToFormat)
		{
#if DEBUG == TRUE
			return stringToFormat.Replace("\n", "").Replace("&#39;", "\'").Replace("&quot;", "\"");
#else
			return stringToFormat.Replace("&#39;", "\'").Replace("&quot;", "\"");

#endif
		}
		#endregion

	};
};
