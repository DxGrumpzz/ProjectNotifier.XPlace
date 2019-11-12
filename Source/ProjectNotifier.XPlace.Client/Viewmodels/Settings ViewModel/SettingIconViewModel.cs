namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System.Windows.Input;

    public class SettingIconViewModel
    {
        public static SettingIconViewModel DesignInstance => new SettingIconViewModel()
        { 
            Description = "תיאור של הגדרה מסויימת",
            Icon = SettingIcon.UserSettings,
        };


        #region Public properties

        /// <summary>
        /// The icon that will be displayed for this setting 
        /// </summary>
        public SettingIcon Icon { get; set; }

        /// <summary>
        /// The settings description 
        /// </summary>
        public string Description { get; set; }

        #endregion


        #region Commands

        public ICommand GotoSettingCommand { get; set; }

        #endregion


        public SettingIconViewModel()
        {
            GotoSettingCommand = new RelayCommand(ExecuteGotoSettingCommand);
        }


        #region Command callbacks

        private void ExecuteGotoSettingCommand()
        {

        } 

        #endregion
    };
};