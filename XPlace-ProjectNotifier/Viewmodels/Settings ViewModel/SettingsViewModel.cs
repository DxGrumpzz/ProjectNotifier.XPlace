namespace XPlace_ProjectNotifier
{
	using System;
	using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public class SettingsViewModel : BaseViewModel
	{

		#region Private fields

		private bool _isOpen;

		private bool _isSaved;

		#endregion


		#region Public properties

		/// <summary>
		/// Application settings
		/// </summary>
		public SettingsModel SettingsModel { get; private set; }

		public TextEntryViewModel<int> ProjectCountSetting { get; set; }
		public TextEntryViewModel<int> NotificationDispalySecondsSetting { get; set; }


		public bool ShowSavedNotification
		{
			get => _isSaved;
			private set
			{
				_isSaved = value;
				OnPropertyChanged();
			}
		}

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


		#endregion


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

				SaveChangesAction = new Action<TextEntryViewModel<int>>(async (setting) =>
				{
					DI.GetLogger().Log($"User changed {nameof(SettingsModel.ProjectsToDisplay)} setting to {setting.Value}", LogLevel.Informative);

					// Update value 
					DI.GetSettings().ProjectsToDisplay = setting.Value;

					// Update config value
					DI.GetService<JsonConfigManager>().WriteSetting(nameof(SettingsModel.ProjectsToDisplay), setting.Value);

					// Show settings saved notification
					await ShowSavedNOtificationAsync();
				}),
			};

			NotificationDispalySecondsSetting = new TextEntryViewModel<int>(SettingsModel.KeepNotificationOpenSeconds)
			{
				IsNumericOnly = true,

				MaxLength = 2,

				ValueValidationAction = new Func<TextEntryViewModel<int>, bool>(setting =>
				{
					// If value didn't change don't update
					if(setting.Value == setting.PreviousValue)
					{
						return false;
					};

					// Boundry validation
					if(setting.Value > 10)
					{
						setting.Value = 10;
						return false;
					}
					else if(setting.Value < 3)
					{
						setting.Value = 3;
						return false;
					};

					return true;
				}),

				SaveChangesAction = new Action<TextEntryViewModel<int>>(async (setting) =>
				{
					DI.GetLogger().Log($"User changed {nameof(SettingsModel.KeepNotificationOpenSeconds)} setting to {setting.Value}", LogLevel.Informative);

					// Update value 
					DI.GetSettings().KeepNotificationOpenSeconds = setting.Value;

					// Update config value
					DI.GetService<JsonConfigManager>().WriteSetting(nameof(SettingsModel.KeepNotificationOpenSeconds), setting.Value);

					// Show settings saved notification
					await ShowSavedNOtificationAsync();
				}),
			};


			CloseSettingsCommand = new RelayCommand(ExecuteCloseSettingsCommand);
		}


		private void ExecuteCloseSettingsCommand()
		{
			IsOpen = false;
		}



		#region Private helpers

		/// <summary>
		/// Animates the settings saved notification
		/// </summary>
		private async Task ShowSavedNOtificationAsync()
		{
			// Show saved notification
			ShowSavedNotification = true;

			// Wait a-bit
			await Task.Delay(1500);

			// Hide notification
			ShowSavedNotification = false;
		}

		#endregion

	};
};