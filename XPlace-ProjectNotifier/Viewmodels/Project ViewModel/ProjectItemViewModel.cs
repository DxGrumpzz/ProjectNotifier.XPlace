namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text;
	using System.Windows;

	/// <summary>
	/// 
	/// </summary>
	public class ProjectItemViewModel : BaseViewModel
	{
		public static ProjectItemViewModel DesignTimeInstance => new ProjectItemViewModel()
		{
			ProjectModel = new ProjectModel()
			{
				Title = "Project title",

				PublishingDate = DateTime.Now,

				Link = "project link",

				Description = "Project descritpion",
			},
		};



		#region Private fields

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ProjectModel _projectModel;

		#endregion

		/// <summary>
		/// A model containing data about the project
		/// </summary>
		public ProjectModel ProjectModel
		{
			get => _projectModel;
			set
			{
				_projectModel = value;
				OnPoropertyChanged();
			}
		}


		public ProjectItemViewModel()
		{

		}
	};
}
