namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Text;

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


		/// <summary>
		/// The name of the main config file
		/// </summary>
		public static string ConfigFileName => CONFIG_FILE_NAME;

		/// <summary>
		/// The name of the log file
		/// </summary>
		public static string LogFileName => LOG_FILE_NAME;

	};
};
