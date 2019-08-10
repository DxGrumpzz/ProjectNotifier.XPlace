namespace XPlace_ProjectNotifier
{
	using System;
	using System.ComponentModel;
	using System.Windows.Controls;

	/// <summary>
	/// Interaction logic for ProjectItemView.xaml
	/// </summary>
	public partial class ProjectItemView : UserControl
	{

		/// <summary>
		/// Desgin time view model
		/// </summary>
		public ProjectItemViewModel designTimeViewModel = new ProjectItemViewModel()
		{
			ProjectModel = new ProjectModel()
			{
				Title = "Project title",

				PublishingDate = DateTime.Now,


				Link = "project link",
				Description = "Project descritpion",
			},
		};

		public ProjectItemView()
		{
			// If control is in design mode, Set desgin time viewtmodel
			if (DesignerProperties.GetIsInDesignMode(this))
			{
				DataContext = designTimeViewModel;
			};

			InitializeComponent();
		}


		public ProjectItemView(ProjectItemViewModel viewModel) :
			this()
		{
			DataContext = viewModel;
		}
	};
}
