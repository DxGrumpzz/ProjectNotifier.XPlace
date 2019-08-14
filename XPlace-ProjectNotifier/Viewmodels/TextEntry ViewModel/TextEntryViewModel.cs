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

		public string _value;

		public string _previousValue;

		#endregion


		#region Public properties

		/// <summary>
		/// A Boolean flag that indicates if this <see cref="TextEntryControl"/> accept only numeric characters
		/// </summary>
		public bool IsNumericOnly { get; set; }

		public string Value
		{
			get => _value;
			set
			{
				_value = value;
				OnPropertyChanged();
			}
		}


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
			if(Value != _previousValue)
			{
				Value = _previousValue;
			};
		}


		private void ExecuteSaveProjectCountCommnad()
		{
			// Update value
			_previousValue = Value;

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
