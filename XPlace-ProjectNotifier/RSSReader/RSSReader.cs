namespace XPlace_ProjectNotifier
{
    using System.Xml;
    using System.Xml.Linq;
	using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// An RSS reader/prases
    /// </summary>
    public class RSSReader
	{
		#region Private fields

		/// <summary>
		/// The url to the Xml document
		/// </summary>
		private string _rssXmlUrl;

		#endregion

		public RSSReader(string rssUrl)
		{
			_rssXmlUrl = rssUrl;
		}


		/// <summary>
		/// Returns an IEnumerable of XElement
		/// </summary>
		/// <param name="descendantsSelection"> The type of descendatns to select </param>
		/// <param name="count"> How many from the top to select </param>
		/// <returns></returns>
		public IEnumerable<XElement> GetXElementNodeList(string descendantsSelection = "item", int count = 25)
		{
			// Select XML nodes from the RSS document
			var descendatsNodes = XDocument.Load(_rssXmlUrl).Descendants(descendantsSelection)
				.Take(count);

			return descendatsNodes;
		}
	}
}
