namespace XPlace_ProjectNotifier
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Xml;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using System.Xml.Linq;

	public class MainWindowViewModel : BaseViewModel
	{

		#region Private fields

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MainWindowModel _model;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool _isLoading = true;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ProjectListViewModel projectListViewModel;

		#endregion

		public MainWindowModel Model
		{
			get => _model;
			set
			{
				_model = value;
				OnPoropertyChanged();
			}
		}

		public ProjectListViewModel ProjectList
		{
			get => projectListViewModel;
			set
			{
				projectListViewModel = value;
				OnPoropertyChanged();

			}
		}


		public bool IsLoading
		{
			get => _isLoading;
			private set
			{
				_isLoading = value;
				OnPoropertyChanged();
			}
		}


		public MainWindowViewModel()
		{
			Task.Run(SetupRSSProjectListAsync);
		}


		#region Private methods

		/// <summary>
		/// Asynchrounsly loads rss feed content
		/// </summary>
		/// <returns></returns>
		private async Task SetupRSSProjectListAsync()
		{
			await Task.Run(() =>
			{
				// Read rss feed
				RSSReader rssReader = new RSSReader("https://www.xplace.com/il/rss/new-projects");

				// Grab the first 25 results from the RSS feed
				var projects = rssReader.GetXElementNodeList(count: 100)
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
							// Replace unicode identifiers(?) string literals
							Title = FormatString(titleNode),
							Description = FormatString(descriptionNode),

							Link = linkNode,

							// Convert the date time to israel standard time
							PublishingDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Parse(publishDateNode), "Israel Standard Time"),
						},
					};
				});

				ProjectList = new ProjectListViewModel()
				{
					ProjectList = new ObservableCollection<ProjectItemViewModel>(projects),
				};
			});

			// Finished loading content
			IsLoading = false;
		}

		#endregion


		#region Private helpers

		/// <summary>
		/// Replaced unicode strings with string literalls
		/// </summary>
		/// <param name="stringToFormat"></param>
		private string FormatString(string stringToFormat)
		{
			return stringToFormat.Replace("&#39;", "\'").Replace("&quot;", "\"");
		}

		#endregion
	}
}