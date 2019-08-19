namespace XPlace_ProjectNotifier
{
	using System;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;

	/// <summary>
	/// 
	/// </summary>
	public class ConsoleLogger : ILoggerBase
	{
	
		/// <summary>
		/// The logger's verboseness level 
		/// </summary>
		public LogLevel LogOutputLevel { get; set; }


		/// <summary>
		/// An action that will be invoked when a new log is logged 
		/// </summary>
		public event Action<string, LogLevel> NewLog = (message, outputLever) => { };


		/// <summary>
		/// Logs the message to console
		/// </summary>
		/// <param name="logMessage"> The message to log </param>
		/// <param name="logLevel"> The log's output severity </param>
		/// <param name="callerOrigin"> Where the logger was called from </param>
		/// <param name="filePath"> The path where the log came from </param>
		/// <param name="lineNumber"> The line number where the log was called </param>
		public void Log(string logMessage, LogLevel logLevel = LogLevel.Normal, [CallerMemberName] string callerOrigin = "", [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
		{
			// Calculate log padding based on:
			// arbitrary space - name of the log level
			string padding = string.Empty.ToString()
			.PadRight(12 - logLevel.ToString().Length, ' ');

			string message = $"[{logLevel}]{Environment.NewLine}" +
				$"[{Path.GetFileName(filePath)} > {callerOrigin}() > Line: {lineNumber}]" + 
				$"{Environment.NewLine}{logMessage}";

			// Log to the console
			Console.WriteLine(message);


			// Invoke event
			NewLog?.Invoke(logMessage, logLevel);
		}
	};
};
