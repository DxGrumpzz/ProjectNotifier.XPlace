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

		#region Private fields

		private SettingsModel _settingsModel;

		public int _currentDisplayedProjectCcount;

		public int _previousProjectCount;

		#endregion


		#region Public properties

		/// <summary>
		/// Application settings
		/// </summary>
		public SettingsModel SettingsModel
		{
			get => _settingsModel;
			set
			{
				_settingsModel = value;
				OnPropertyChanged();
			}
		}

		public int CurrentDisplayedProjectCcount
		{
			get => _currentDisplayedProjectCcount;
			set
			{
				_currentDisplayedProjectCcount = value;
				OnPropertyChanged();
			}
		}

		#endregion


		#region Commands

		public RelayCommand SaveProjectCountCommnad { get; }
		public RelayCommand LostFocusCommand { get; set; }
		public RelayCommand RemoveFocusCommand { get; set; }

		#endregion


		public SettingsViewModel(SettingsModel settings)
		{
			SettingsModel = settings;


			CurrentDisplayedProjectCcount = settings.ProjectsToDisplay;
			_previousProjectCount = CurrentDisplayedProjectCcount;


			SaveProjectCountCommnad = new RelayCommand(ExecuteSaveProjectCountCommnad);
			LostFocusCommand = new RelayCommand(ExecuteLostFocusCommand);
			RemoveFocusCommand = new RelayCommand(ExecuteRemoveFocusCommand);
		}

		public SettingsViewModel()
		{

		}

		private void ExecuteLostFocusCommand()
		{
			if(CurrentDisplayedProjectCcount != _previousProjectCount)
			{
				CurrentDisplayedProjectCcount = _previousProjectCount;
			};
		}


		private void ExecuteSaveProjectCountCommnad()
		{
			// value validation
			if(CurrentDisplayedProjectCcount > 100)
			{
				CurrentDisplayedProjectCcount = 100;
			}
			else if(CurrentDisplayedProjectCcount < 25)
			{
				CurrentDisplayedProjectCcount = 25;
			};


			// Update value
			SettingsModel.ProjectsToDisplay = CurrentDisplayedProjectCcount;
			_previousProjectCount = CurrentDisplayedProjectCcount;

			// Remove focus 
			ExecuteRemoveFocusCommand();
		}

		private void ExecuteRemoveFocusCommand()
		{
			// Remove focus
			Keyboard.ClearFocus();

			// Reset value
			ExecuteLostFocusCommand();
		}

	};
};