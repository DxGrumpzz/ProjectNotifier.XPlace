namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// url's and links that are used by this app
	/// </summary>
	public static class AppLinks
	{

		/// <summary>
		/// Link to the RSS feed that is consumed by this app
		/// </summary>
#if DEBUG == TRUE
		public static string RSSFeedUrl => @"C:\Development\Projects\XPlace-ProjectNotifier\Mock-RSS\ProjectList.xml";
#else
		public static string RSSFeedUrl => "https://www.xplace.com/il/rss/new-projects";
#endif

	};
};
