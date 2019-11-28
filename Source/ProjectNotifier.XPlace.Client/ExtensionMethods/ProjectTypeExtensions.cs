namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Diagnostics;

    /// <summary>
    /// A Helper class that provides some extension methods for <see cref="ProjectType"/>
    /// </summary>
    public static class ProjectTypeExtensions
    {
        /// <summary>
        /// Convert a <see cref="ProjectType"/> to it's hebrew iquivilant
        /// </summary>
        /// <param name="projectType"></param>
        /// <returns></returns>
        public static string ToHebrewString(this ProjectType projectType)
        {
            switch (projectType)
            {
                case ProjectType.WebDevelopment:
                    return "פיתוח אתרים";

                case ProjectType.DesktopDevelopment:
                    return "פיתוח תוכנה";

                case ProjectType.MobileDevelopment:
                    return "פיתוח מובייל";

                case ProjectType.SysAdmins:
                    return "ניהול מערכות";

                case ProjectType.FullStackDevelopment:
                    return "פיתוח פולסטאק";

                case ProjectType.BackendDevelopment:
                    return "פיתוח צד שרת";

                case ProjectType.FrontEndDevelopment:
                    return "פיתוח צד לקוח";

                case ProjectType.VideoEditing:
                    return "עריכת וידאו";

                case ProjectType.Filming:
                    return "צילום וידאו";

                case ProjectType.Electronics:
                    return "אלקטרוניקה";

                case ProjectType.BlockchainAndCryptoCurrencies:
                    return "בלוקצ'איין ומטבעות קריפטו";

                case ProjectType.DesignAndGraphics:
                    return "עיצוב וגרפיקה";

                case ProjectType.Technologies:
                    return "טכנולוגיות";

                case ProjectType.WritingAndEditing:
                    return "כתיבה ועריכה";

                case ProjectType.Translations:
                    return "תרגום";

                case ProjectType.MarketingAndSales:
                    return "שיווק ומכריות";

                case ProjectType.Photography:
                    return "צילום תמונות";

                case ProjectType.Administration:
                    return "אדמיניסטרציה";

                case ProjectType.TeachersAndProfessors:
                    return "מרצים ופרופסורים";

                case ProjectType.Executives:
                    return "בכירים";

                case ProjectType.CoachingAndTraining:
                    return "קואצ'יגן ואימונים";

                case ProjectType.ArchitectureAndInteriorDesign:
                    return "אדריכלות ועיצוב פנים";

                case ProjectType.Financial:
                    return "כספים";

                case ProjectType.LawyersAndLawServices:
                    return "עו\"ד ושירותים משפטיים";

                case ProjectType.SoundAndMusic:
                    return "סאונד ומוזקיה";

                case ProjectType.Engineering:
                    return "הנדסה";

                case ProjectType.PrototypesAndManufacturing:
                    return "אבי טיפוס וייצור";

                case ProjectType.ProductionsAndShows:
                    return "הפקה, הצגות, וטלויזיה";

                case ProjectType.SAP:
                    return "סאפ";

                default:
                {
                    Debugger.Break();
                    return null;
                };
            };
        }
    };
};
