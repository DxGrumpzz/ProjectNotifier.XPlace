namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.ObjectModel;
	using System.Linq;

	/// <summary>
	/// A project loader
	/// </summary>
	public class ProjectLoader
	{
		#region Private fields

		private bool _isLoadingProjects = false;

		#endregion


		public double Interval { get; private set; }

		public SettingsModel SettingsModel { get; }


		public ProjectLoader(double interval, SettingsModel settingsModel)
		{
			Interval = interval;
			SettingsModel = settingsModel;
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
