namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Text;
    using System.Windows.Input;

    /// <summary>
    /// 
    /// </summary>
    public class TextEntryViewModel : BaseViewModel
	{

		#region Private fields

		private SettingsModel _settingsModel;

		public int _currentDisplayedProjectCcount;

		public int _previousProjectCount;

		#endregion


		#region Public properties
		public int CurrentDisplayedProjectCcount
		{
			get => _currentDisplayedProjectCcount;
			set
			{
				_currentDisplayedProjectCcount = value;
				OnPropertyChanged();
			}
		}


		/// <summary>
		/// A Boolean flag that indicates if this <see cref="TextEntryControl"/> accept only numeric characters
		/// </summary>
		public bool IsNumericOnly { get; set; }

		#endregion


		#region Commands

		public RelayCommand SaveProjectCountCommnad { get; }
		public RelayCommand LostFocusCommand { get; set; }
		public RelayCommand RemoveFocusCommand { get; set; }

		#endregion


		public TextEntryViewModel()
		{
			SaveProjectCountCommnad = new RelayCommand(ExecuteSaveProjectCountCommnad);
			LostFocusCommand = new RelayCommand(ExecuteLostFocusCommand);
			RemoveFocusCommand = new RelayCommand(ExecuteRemoveFocusCommand);
		}


		#region Command callbacks

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

		#endregion


	};
};
