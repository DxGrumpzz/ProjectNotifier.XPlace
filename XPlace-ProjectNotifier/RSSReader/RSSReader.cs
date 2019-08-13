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
		string _rssXmlUrl;

		XmlDocument _rssDocumnet;

		#endregion

		public RSSReader(string rssUrl)
		{
			// Instanciate the Document
			_rssDocumnet = new XmlDocument();
			// Load the RSS document 
			_rssDocumnet.Load(rssUrl);

			_rssXmlUrl = rssUrl;
		}


		/// <summary>
		/// Returns an <see cref="XmlNodeList"/> that contains information about items in the document
		/// </summary>
		/// <param name="nodesToSelect"> node selection "filter" </param>
		public IEnumerable<XmlNode> GetXmlNodeList(string nodesToSelect = "rss/channel/item", int count = 25)
		{
			var rssNodes = _rssDocumnet.SelectNodes(nodesToSelect)
				.Cast<XmlNode>()
				.Take(count);

			return rssNodes;
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
