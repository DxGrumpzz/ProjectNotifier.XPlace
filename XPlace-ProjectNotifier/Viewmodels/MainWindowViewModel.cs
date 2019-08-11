namespace XPlace_ProjectNotifier
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Text;
	using System.Xml;
	using System.Diagnostics;
	using System.Windows;

	public class MainWindowViewModel : BaseViewModel
	{

		#region Private fields

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MainWindowModel _model;

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

		public ProjectListViewModel ProjectList { get; private set; }


		public MainWindowViewModel()
		{
			SetupRSSProjectList();
		}


		#region Private methods

		private void SetupRSSProjectList()
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

				// Replace xml &quot; with actuall text quotes
				titleNode.Replace("&quot;", "\"");
				descriptionNode.Replace("&quot;", "\"");

				return new ProjectModel()
				{
					Title = titleNode,

					Link = linkNode,

					Description = descriptionNode,

					PublishingDate = DateTime.Parse(publishDateNode),
				};
			})
			// Further convert the ProjectModel into a ProjectItemViewModel;
			.Select(project =>
			{
				return new ProjectItemViewModel()
				{
					ProjectModel = project,
				};
			});


			ProjectList = new ProjectListViewModel()
			{
				ProjectList = new ObservableCollection<ProjectItemViewModel>(projects),
			};
		}

		#endregion

	}
}