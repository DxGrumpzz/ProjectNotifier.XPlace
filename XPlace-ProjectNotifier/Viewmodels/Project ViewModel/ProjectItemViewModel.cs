namespace XPlace_ProjectNotifier
{
	using System;
	using System.Diagnostics;

	/// <summary>
	/// 
	/// </summary>
	public class ProjectItemViewModel : BaseViewModel
	{
		public static ProjectItemViewModel DesignTimeInstance => new ProjectItemViewModel()
		{
			ProjectModel = new ProjectModel()
			{
				Title = "כותרת פרוייקט",

				PublishingDate = DateTime.Now,

				Link = "https://www.xplace.com/il/job/123456",

				Description = "תיאור פרוייקט",
			},
		};


		#region Private fields

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ProjectModel _projectModel;

		#endregion

		#region Public properties

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


		#endregion

		#region Commands
		public RelayCommand OpenProjectUrlCommand { get; }

		#endregion


		public ProjectItemViewModel()
		{
			OpenProjectUrlCommand = new RelayCommand(ExecuteOpenProjectUrlCommand);
		}



		#region Command callbacks


		/// <summary>
		/// Opens the browser with the associated project
		/// </summary>
		private void ExecuteOpenProjectUrlCommand()
		{
			// Open project link in Default browser
			// (thank you .net core 3 for needing to open a cmd to launch a url)
			Process.Start(new ProcessStartInfo
			{
				FileName = "cmd",
				Arguments = $"/c start {ProjectModel.Link}",
				// Ensures that the user doesn't see the cmd window opening
				WindowStyle = ProcessWindowStyle.Hidden,
				UseShellExecute = false,
				CreateNoWindow = true,
			});
		}

		#endregion

	};
}