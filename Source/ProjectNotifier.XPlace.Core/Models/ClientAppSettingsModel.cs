namespace ProjectNotifier.XPlace.Core
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// A model class that contains the app's settings
	/// </summary>
	public class ClientAppSettingsModel
	{

		/// <summary>
		/// How many projects to read/display
		/// </summary>
		public int ProjectsToDisplay { get; set; }

		/// <summary>
		/// How long to display the new projects notification in seconds
		/// </summary>
		public int KeepNotificationOpenSeconds { get; set; }

	};
}
