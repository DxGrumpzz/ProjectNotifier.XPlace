namespace ProjectNotifier.XPlace.WebServer
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Linq;

    /// <summary>
    /// The client's implementation of a project loader
    /// </summary>
    public class ProjectLoader : IProjectLoader
    {

        #region Private fields

        private readonly string _url;

        private readonly IProjectTypeProcessor _projectTypeProcessor;

        #endregion


        public ProjectLoader(IProjectTypeProcessor projectTypeProcessor, string url)
        {
            _projectTypeProcessor = projectTypeProcessor;
            _url = url;
        }


        /// <summary>
        /// Loads a list of projects from somewhere
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProjectModel>> LoadProjectsAsync(int count = 100, CancellationToken cancellationToken = default)
        {
            // Grab however many results the user requested from the RSS feed
            var projects = (await GetXElementNodeListAsync(count: count, cancellationToken: cancellationToken))
            // "Convert" the xml data to a ProjectModel
            .Select(element =>
            {
                // Select required nodes
                var titleNode = element.Element("title").Value;
                var linkNode = element.Element("link").Value;
                var descriptionNode = element.Element("description").Value;
                var publishDateNode = element.Element("pubDate").Value;
                var projectID = Convert.ToInt32(element.Element("guid").Value);


                return new ProjectModel()
                {
                    // Replace unicode identifiers(?) with string literals
                    Title = NormalizeString(titleNode),
                    Description = NormalizeString(descriptionNode),

                    Link = linkNode,

                    // Convert the date time to israel standard time
                    PublishingDate = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Parse(publishDateNode), "Israel Standard Time"),

                    ProjectID = projectID,
                };
            })
            .ToList();

            projects
            .ForEach(project =>
            {
                var projectTypes = _projectTypeProcessor.GetProjectType(project);

                project.ProjectTypes = projectTypes;
            });

            return projects;
        }


        #region Private helpers

        /// <summary>
        /// Returns an IEnumerable of XElement
        /// </summary>
        /// <param name="descendantsSelection"> The type of descendatns to select </param>
        /// <param name="count"> How many from the top to select </param>
        /// <returns></returns>
        private async Task<IEnumerable<XElement>> GetXElementNodeListAsync(string descendantsSelection = "item", int count = 25, CancellationToken cancellationToken = default)
        {
            using (XmlReader xmlReader = XmlReader.Create(_url, new XmlReaderSettings()
            {
                Async = true,
            }))
            {
                // Select XML nodes from the RSS document
                var descendatsNodes = (await XDocument.LoadAsync(xmlReader, LoadOptions.None, cancellationToken))
                    .Descendants(descendantsSelection)
                    .Take(count);

                return descendatsNodes;
            };
        }


        /// <summary>
        /// Replaced unicode strings with string literalls
        /// </summary>
        /// <param name="stringToFormat"></param>
        private string NormalizeString(string stringToFormat)
        {
#if DEBUG == TRUE
            return stringToFormat.Replace("\n", "").Replace("&#39;", "\'").Replace("&quot;", "\"");
#else
			return stringToFormat.Replace("&#39;", "\'").Replace("&quot;", "\"");
#endif
        }

        #endregion

    };
};