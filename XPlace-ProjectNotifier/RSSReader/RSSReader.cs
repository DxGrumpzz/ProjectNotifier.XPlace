namespace XPlace_ProjectNotifier
{
    using System.Xml;
    using System.Xml.Linq;
	using System.Linq;

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
		public XmlNodeList GetNodeFromRssDocument(string nodesToSelect = "rss/channel/item")
		{
			// Select XML nodes from the RSS document
			XmlNodeList rssNodes = _rssDocumnet.SelectNodes(nodesToSelect);

			return rssNodes;
		}

	}
}
