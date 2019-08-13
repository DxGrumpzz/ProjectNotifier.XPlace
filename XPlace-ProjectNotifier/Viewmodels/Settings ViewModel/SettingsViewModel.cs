namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Windows.Input;

	/// <summary>
	/// 
	/// </summary>
	public class SettingsViewModel : BaseViewModel
	{

		#region Private fields

		private SettingsModel _settingsModel;

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


		public int PreviousProjectCount { get; private set; }


		public int _currentDisplayedProjectCcount;
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

		#endregion


		public SettingsViewModel(SettingsModel settings)
		{
			SettingsModel = settings;


			CurrentDisplayedProjectCcount = settings.ProjectsToDisplay;
			PreviousProjectCount = CurrentDisplayedProjectCcount;


			SaveProjectCountCommnad = new RelayCommand(ExecuteSaveProjectCountCommnad);
			LostFocusCommand = new RelayCommand(ExecuteLostFocusCommand);
		}


		public SettingsViewModel()
		{

		}


		private void ExecuteLostFocusCommand()
		{
			if(CurrentDisplayedProjectCcount != PreviousProjectCount)
			{
				CurrentDisplayedProjectCcount = PreviousProjectCount;
			};
		}


		private void ExecuteSaveProjectCountCommnad()
		{
			SettingsModel.ProjectsToDisplay = CurrentDisplayedProjectCcount;
			PreviousProjectCount = CurrentDisplayedProjectCcount;
		}
	};
};