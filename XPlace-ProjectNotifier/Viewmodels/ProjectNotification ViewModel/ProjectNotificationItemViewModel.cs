namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;

	/// <summary>
	/// 
	/// </summary>
	public class ProjectNotificationItemViewModel
	{
		
		#region Public property

		/// <summary>
		/// The ProjectModel, contains info about the new projet
		/// </summary>
		public ProjectModel ProjectModel { get; set; }

		#endregion


		#region Commands

		public RelayCommand OpenProjectCommand { get; }

		#endregion

		public ProjectNotificationItemViewModel()
		{
			OpenProjectCommand = new RelayCommand(ExecuteOpenProjectCommand);
		}


		#region Command callbacks

		private void ExecuteOpenProjectCommand()
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
};
