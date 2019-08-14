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

		public TextEntryViewModel<int> ProjectCountSetting { get; set; }


		public SettingsViewModel(SettingsModel settings)
		{
			SettingsModel = settings;

			ProjectCountSetting = new TextEntryViewModel<int>(SettingsModel.ProjectsToDisplay)
			{
				IsNumericOnly = true,

				MaxLength = 3,

				ValueValidationAction = new Func<TextEntryViewModel<int>, bool>(value =>
				{

					if(value.Value > 100)
					{
						value.Value = 100;
						return false;
					}
					else if(value.Value < 25)
					{
						value.Value = 25;
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