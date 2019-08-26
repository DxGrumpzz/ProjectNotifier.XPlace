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

		public ProjectModel ProjectModel { get; set; }


		public RelayCommand OpenProjectCommand { get; }


		public ProjectNotificationItemViewModel()
		{
			OpenProjectCommand = new RelayCommand(ExecuteOpenProjectCommand);
		}


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
	};
};
