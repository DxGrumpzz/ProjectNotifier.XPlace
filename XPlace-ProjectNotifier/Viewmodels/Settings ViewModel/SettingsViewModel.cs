namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
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





		public SettingsViewModel(SettingsModel settings)
		{
			SettingsModel = settings;

		}

		public SettingsViewModel()
		{

		}
	};
};