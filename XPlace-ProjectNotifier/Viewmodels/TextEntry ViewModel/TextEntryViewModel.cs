namespace XPlace_ProjectNotifier
{
	using System;
	using System.Windows.Input;

	/// <summary>
	/// 
	/// </summary>
	public class TextEntryViewModel : BaseViewModel
	{

		#region Private fields

		private string _value;

		#endregion


		#region Public properties

		public string PreviousValue { get; set; }


		/// <summary>
		/// A Boolean flag that indicates if this <see cref="TextEntryControl"/> accept only numeric characters
		/// </summary>
		public bool IsNumericOnly { get; set; }

		/// <summary>
		/// How many characters/numbers are allowed to be entered
		/// </summary>
		public int MaxLength { get; set; } = 10;

		public string Value
		{
			get => _value;
			set
			{
				_value = value;
				OnPropertyChanged();
			}
		}


		/// <summary>
		/// A validation action that will be executed before the value changes
		/// </summary>
		public Func<TextEntryViewModel, bool> ValueValidationAction { get; set; }

		#endregion


		#region Commands

		public RelayCommand SaveProjectCountCommnad { get; }
		public RelayCommand LostFocusCommand { get; set; }
		public RelayCommand RemoveFocusCommand { get; set; }

		#endregion


		public TextEntryViewModel(string value)
		{
			Value = value;
			PreviousValue = value;


			SaveProjectCountCommnad = new RelayCommand(ExecuteSaveProjectCountCommnad);
			LostFocusCommand = new RelayCommand(ExecuteLostFocusCommand);
			RemoveFocusCommand = new RelayCommand(ExecuteRemoveFocusCommand);
		}


		#region Command callbacks

		private void ExecuteLostFocusCommand()
		{
			// If focus was lost before changing the value
			// set current value to previous
			if(Value != PreviousValue)
			{
				Value = PreviousValue;
			};
		}

		private void ExecuteSaveProjectCountCommnad()
		{
			// Validate setting value
			if(ValueValidationAction?.Invoke(this) == true)
			{
				// Update value
				PreviousValue = Value;
			}

			// Remove focus 
			Keyboard.ClearFocus();
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
