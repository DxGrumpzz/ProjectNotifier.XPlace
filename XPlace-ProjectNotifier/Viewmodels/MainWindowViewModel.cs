namespace XPlace_ProjectNotifier
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Xml;
	using System.Diagnostics;
	using System.Threading.Tasks;

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
			set
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
				var nodes = rssReader.GetNodeFromRssDocument();

				// Select first 25 results
				var projects = nodes.Cast<XmlNode>().Take(25)
				// "Convert" the xml data to a ProjectModel
				.Select(node =>
				{
					// Select required nodes
					var titleNode = node.SelectSingleNode("title").InnerText;
					var linkNode = node.SelectSingleNode("link").InnerText;
					var descriptionNode = node.SelectSingleNode("description").InnerText;
					var publishDateNode = node.SelectSingleNode("pubDate").InnerText;

					// Replace unicode identifiers(?) string literals
					titleNode = FormatString(titleNode);
					descriptionNode = FormatString(descriptionNode);

					return new ProjectModel()
					{
						Title = titleNode,

						Link = linkNode,

						Description = descriptionNode,

						PublishingDate = DateTime.Parse(publishDateNode),
					};
				})
				// Further convert the ProjectModel into a ProjectItemViewModel;
				.Select(project => new ProjectItemViewModel()
				{
					ProjectModel = project,
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