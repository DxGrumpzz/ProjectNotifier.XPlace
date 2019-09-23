namespace ProjectNotifier.XPlace.Client
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
		public static string RSSFeedUrl => @$"../../../../../../Misc/Mock RSS feed.xml";
#else
		public static string RSSFeedUrl => "https://www.xplace.com/il/rss/new-projects";
#endif

	};
};
