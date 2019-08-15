namespace XPlace_ProjectNotifier
{
	using System;
    using System.Diagnostics;

    /// <summary>
    /// 
    /// </summary>
    public class SettingsViewModel : BaseViewModel
	{

		#region Private fields

		private bool _isOpen;

		#endregion

		/// <summary>
		/// Application settings
		/// </summary>
		public SettingsModel SettingsModel { get; private set; }

		public TextEntryViewModel<int> ProjectCountSetting { get; set; }


		/// <summary>
		/// A boolean flag that indicates if this control is open or in view
		/// </summary>
		public bool IsOpen
		{
			get => _isOpen;
			set
			{
				_isOpen = value;
				OnPropertyChanged();
			}
		}


		public RelayCommand CloseSettingsCommand { get; }


		public SettingsViewModel() { }

		public SettingsViewModel(SettingsModel settings)
		{
			SettingsModel = settings;

			ProjectCountSetting = new TextEntryViewModel<int>(SettingsModel.ProjectsToDisplay)
			{
				IsNumericOnly = true,

				MaxLength = 3,

				ValueValidationAction = new Func<TextEntryViewModel<int>, bool>(setting =>
				{
					// If value didn't change don't update
					if(setting.Value == setting.PreviousValue)
					{
						return false;
					};

					// Boundry validation
					if(setting.Value > 100)
					{
						setting.Value = 100;
						return false;
					}
					else if(setting.Value < 25)
					{
						setting.Value = 25;
						return false;
					};


					return true;
				}),

				SaveChangesAction = new Action<TextEntryViewModel<int>>(value =>
				{
					// Update config value if succesfull
					DI.GetService<JsonConfigManager>().WriteSetting(nameof(SettingsModel.ProjectsToDisplay), value.Value);
				}),
			};

			CloseSettingsCommand = new RelayCommand(ExecuteCloseSettingsCommand);
		}


		private void ExecuteCloseSettingsCommand()
		{
			IsOpen = false;
		}

	};
};