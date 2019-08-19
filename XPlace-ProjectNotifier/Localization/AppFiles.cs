namespace XPlace_ProjectNotifier
{
	using System;

	/// <summary>
	/// A static class that contains Files and paths to files that are used/required by this app
	/// </summary>
	public static class AppFiles
	{
		#region Constants

		/// <summary>
		/// The name of the main config file
		/// </summary>
		private const string CONFIG_FILE_NAME = "Config.json";

		/// <summary>
		/// The name of the log file
		/// </summary>
		private const string LOG_FILE_NAME = "Log.txt";

		#endregion

		#region private fields

		private static string _pathToApp = Environment.CurrentDirectory;

		#endregion


		/// <summary>
		/// The name of the main config file
		/// </summary>
		public static string ConfigFileName => CONFIG_FILE_NAME;

		/// <summary>
		/// The path to the app's log file
		/// </summary>
		public static string LogFilePath => $"{_pathToApp}\\{LOG_FILE_NAME }";

	};
};
