namespace ProjectNotifier.XPlace.WebServer
{
    using ProjectNotifier.XPlace.Core;

    using System.Collections.Generic;

    /// <summary>
    /// A processor of text used to determine the type of a project, Whether it is development, desgin, and others...
    /// </summary>
    public class ProjectTypeProcessor : IProjectTypeProcessor
    {
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
        public IEnumerable<ProjectType> GetProjectType(ProjectModel project)
        {
            // Store the found types, 
            // Using HashSet to ensure no duplicates
            HashSet<ProjectType> projectTypeList = new HashSet<ProjectType>();

            // Holds the project's title as an upper case string 
            string projectTitleAsUpper = project.Title.ToUpper();

            _keyWords
            // Iterate over the keywords
            .ForEach(keyValue =>
            {
                // Check if keyword exists in the title
                if(projectTitleAsUpper.Contains(keyValue.Key) == true) 
                {
                    // If it exist(s) add 
                    projectTypeList.UnionWith(keyValue.Value);
                };
            });

            return projectTypeList;
        }
    };
};