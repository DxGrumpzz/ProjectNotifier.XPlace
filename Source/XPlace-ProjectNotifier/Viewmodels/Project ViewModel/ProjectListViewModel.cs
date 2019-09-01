namespace XPlace_ProjectNotifier
{
    using System;
    using System.Collections.ObjectModel;

	/// <summary>
	/// 
	/// </summary>
	public class ProjectListViewModel : BaseViewModel
	{

		public static ProjectListViewModel DesignTimeInstance => new ProjectListViewModel()
		{
			ProjectList = new ObservableCollection<ProjectItemViewModel>()
			{
				new ProjectItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project title",

						PublishingDate = DateTime.Now,

						Link = "project link",

						Description = "Project descritpion",
					},
				},
				new ProjectItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project title",

						PublishingDate = DateTime.Now,

						Link = "project link",

						Description = "Project descritpion",
					},
				},
				new ProjectItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project title",

						PublishingDate = DateTime.Now,

						Link = "project link",

						Description = "Project descritpion",
					},
				},
				new ProjectItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project title",

						PublishingDate = DateTime.Now,

						Link = "project link",

						Description = "Project descritpion",
					},
				},
			},
		};

		#region Private fields

		private ObservableCollection<ProjectItemViewModel> _projectList;

		#endregion


		/// <summary>
		/// A List of projects
		/// </summary>
		public ObservableCollection<ProjectItemViewModel> ProjectList
		{
			get => _projectList;
			set
			{
				_projectList = value;
				OnPropertyChanged();
			}
		}

	};
}