namespace XPlace_ProjectNotifier
{
	using System;
    using System.IO;
    using System.Runtime.CompilerServices;

	/// <summary>
	/// A logger that is responsible for logging info to a file
	/// </summary>
	public class FileLogger : ILoggerBase
	{
		/// <summary>
		/// The logger's verboseness level 
		/// </summary>
		public LogLevel LogOutputLevel { get; set; }


		/// <summary>
		/// An action that will be invoked when a new log is logged 
		/// </summary>
		public event Action<string, LogLevel> NewLog = (message, outputLever) => { };


		public FileLogger()
		{
			// Create new file for logging
			File.Create(AppFiles.LogFilePath)
				// Dispose file stream after creation
				.Dispose();
		}

		
		/// <summary>
		/// Logs the message to a file
		/// </summary>
		/// <param name="logMessage"> The message to log </param>
		/// <param name="logLevel"> The log's output severity </param>
		/// <param name="callerOrigin"> Where the logger was called from </param>
		/// <param name="filePath"> The path where the log came from </param>
		/// <param name="lineNumber"> The line number where the log was called </param>
		public void Log(string logMessage, LogLevel logLevel = LogLevel.Normal, [CallerMemberName] string callerOrigin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			// Setup log message format
			string message = 
				$"[{logLevel}]{Environment.NewLine}" +
				$"[{Path.GetFileName(filePath)} > {callerOrigin}() > Line: {lineNumber}]" +
				$"{Environment.NewLine}{logMessage}{Environment.NewLine}";

			
			// Write log message to log file
			File.AppendAllText(AppFiles.LogFilePath, message);


			// Invoke log event
			NewLog?.Invoke(logMessage, logLevel);
		}
	};
};
