namespace ProjectNotifier.XPlace.WebServer
{
    using System.Collections.Generic;
    using ProjectNotifier.XPlace.Core;
    using System.Linq;

    /// <summary>
    /// A processor of text used to determine the type of a project, Whether it is development, desgin, and others...
    /// </summary>
    public class ProjectTypeProcessor : IProjectTypeProcessor
    {

        /// <summary>
        /// A list of keywords to ignore in the project's title and description
        /// </summary>
        private string[] ignoreKeywords = new string[]
        {
            "דרוש",
            "דרושים",
            "דרושה",
            "פרילנסרים",
            "מומחה",
            "עם",
            "אלופים",
            "מרגשת",
            "צעירה",

        };


        /// <summary>
        /// A list of symbols to ignore in the title/description
        /// </summary>
        private string[] ignoreSymbols = new[]
        {
            " ",
            "/",
            "\\",
            ".",
            ",",
            "\t",
            "-",
            "!",
            "(",
            ")",
        };


        /// <summary>
        /// A dictionary list that contains project keywords and thier respective project Types
        /// </summary>
        private Dictionary<string, ProjectType[]> _keyWords = new Dictionary<string, ProjectType[]>()
        {
            { "קוד", new[] { ProjectType.DesktopDevelopment, ProjectType.WebDevelopment, } },
            { "C#", new[] { ProjectType.DesktopDevelopment, ProjectType.WebDevelopment, } },
            { "WPF", new[] { ProjectType.DesktopDevelopment, } },
            { "אתר", new[] { ProjectType.WebDevelopment, } },
            { "מפתח", new[] { ProjectType.DesktopDevelopment, ProjectType.WebDevelopment, } },
            { "תוכנה", new[] { ProjectType.DesktopDevelopment, } },
            { "אנדרואיד", new[] { ProjectType.MobileDevelopment, } },
            { "ANDROID", new[] { ProjectType.MobileDevelopment, } },
            { "מובייל", new[] { ProjectType.MobileDevelopment, } },
            { "IOS", new[] { ProjectType.MobileDevelopment, } },
            { "API", new[] { ProjectType.BackendDevelopment, } },
            { "PHP", new[] { ProjectType.BackendDevelopment, } },
            { "FULL STACK", new[] { ProjectType.BackendDevelopment, } },
            { "FULLSTACK", new[] { ProjectType.BackendDevelopment, } },

            { "מהנדס", new[] { ProjectType.Engineering, } },
            { "מהנדסים", new[] { ProjectType.Engineering, } },
            { "הנדסאי", new[] { ProjectType.Engineering, } },
            { "הנדסאים", new[] { ProjectType.Engineering, } },


            { "מוזיקה", new[] { ProjectType.SoundAndMusic, } },
            { "מוזיקאי", new[] { ProjectType.SoundAndMusic, } },


            { "מרצה", new[] { ProjectType.TeachersAndProfessors, } },
            { "מרצים", new[] { ProjectType.TeachersAndProfessors, } },


            { "עיצוב", new[] { ProjectType.DesignAndGraphics, } },
            { "מעצב", new[] { ProjectType.DesignAndGraphics, } },
            { "מעצבת", new[] { ProjectType.DesignAndGraphics, } },
            { "גרפיקה", new[] { ProjectType.DesignAndGraphics, } },
            { "SolidWorks", new[] { ProjectType.DesignAndGraphics, } },
            { "DESIGN", new[] { ProjectType.DesignAndGraphics, } },


            { "עריכה", new[] { ProjectType.WritingAndEditing, } },
            { "עריכת", new[] { ProjectType.WritingAndEditing, } },
            { "כתיבת", new[] { ProjectType.WritingAndEditing, } },
            { "כתיבה", new[] { ProjectType.WritingAndEditing, } },


            { "תרגום", new[] { ProjectType.Translations, } },


            { "וידאו", new[] { ProjectType.Filming, } },
            { "סרטון", new[] { ProjectType.Filming, } },
            { "צילום", new[] { ProjectType.Filming, } },

            { "מכירות", new[] { ProjectType.MarketingAndSales, } },
        };



        public ProjectTypeProcessor()
        {

        }

        /// <summary>
        /// Task a <see cref="ProjectModel"/> and returns a list of types the <paramref name="project"/> fits in
        /// </summary>
        /// <param name="project"> The project to check </param>
        /// <returns></returns>
        public IEnumerable<ProjectType> GetProjectType(in ProjectModel project)
        {
            // Store the found types, Using HashSet to ensure no duplicates
            HashSet<ProjectType> projectTypeList = new HashSet<ProjectType>();


            // Process project title

            // Get the title as a borken down list of words
            var titleKeywords = project.Title
            // Split the title based on the ignored symbols
            .Split(ignoreSymbols, System.StringSplitOptions.RemoveEmptyEntries)
            .ToList();

            // Takes a list of title keyword an normalizes them
            NormalizeHebrewWords(titleKeywords);


            // Remove unnecessary words
            ignoreKeywords
            .ToList()
            // For every unnecessary word 
            .ForEach(keyword =>
            {
                // Check if word exists in the title and remove it
                titleKeywords.Remove(keyword);
            });


            // Check if title keyword exists in Keywords dictionary
            titleKeywords
            .ForEach(word =>
            {
                // Find matching keywords in project's title
                _keyWords.TryGetValue(word.ToUpper(), out ProjectType[] projectTypes);



                // If there are matching keywords
                if (projectTypes != null)
                {
                    // Add them to the list
                    projectTypeList.UnionWith(projectTypes);
                };
            });


            return projectTypeList;
        }


        /// <summary>
        /// Takes a <see cref="List"/> of <see cref="string "/> that contains a project's title and removes problematic hebrew characters
        /// </summary>
        /// <param name="titleKeywords">  </param>
        private void NormalizeHebrewWords(List<string> titleKeywords)
        {
            var problematicHebrewWords = titleKeywords
            // Checks if the begining of the word has set characters
            .Where(word => word[0] == 'ל' || word[0] == 'ו')
            .ToList();

            // Remove the words from the title keywords
            problematicHebrewWords
            .ForEach(word => titleKeywords.Remove(word));

            // Retreive the problematic words as normalized words
            var nonProblematicHebrewWords = problematicHebrewWords
            // Remove the first letter
            .Select(word => word = word.Remove(0, 1));

            // Add the normalized words back to the title keywords
            titleKeywords.AddRange(nonProblematicHebrewWords);
        }

    };
};