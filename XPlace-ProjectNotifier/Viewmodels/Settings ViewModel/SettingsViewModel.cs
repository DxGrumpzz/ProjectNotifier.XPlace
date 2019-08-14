namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Text;
	using System.Windows.Controls;
	using System.Windows.Input;

	/// <summary>
	/// 
	/// </summary>
	public class SettingsViewModel : BaseViewModel
	{

		/// <summary>
		/// Application settings
		/// </summary>
		public SettingsModel SettingsModel { get; private set; }

		public TextEntryViewModel ProjectCountSetting { get; set; }


		public SettingsViewModel(SettingsModel settings)
		{
			SettingsModel = settings;

			ProjectCountSetting = new TextEntryViewModel(SettingsModel.ProjectsToDisplay.ToString())
			{
				IsNumericOnly = true,

				ValueValidationAction = new Func<TextEntryViewModel, bool>(value =>
				{
					int intValue = Convert.ToInt32(value.Value);

					if(intValue > 100)
					{
						value.Value = "100";
						return false;
					}
					else if(intValue < 25)
					{
						value.Value = "25";
						return false;
					};

					return true;
				}),
			};
		}

		public SettingsViewModel()
		{

		}
	};
};