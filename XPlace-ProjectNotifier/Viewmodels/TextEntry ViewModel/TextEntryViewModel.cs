namespace XPlace_ProjectNotifier
{
	using System;
    using System.Collections.Generic;
    using System.Windows.Input;

	/// <summary>
	/// 
	/// </summary>
	public class TextEntryViewModel<T> : BaseViewModel
	{

		#region Private fields

		private T _value;
		private T _previousValue { get; set; }

		#endregion


		#region Public properties

		/// <summary>
		/// A Boolean flag that indicates if this <see cref="TextEntryControl"/> accept only numeric characters
		/// </summary>
		public bool IsNumericOnly { get; set; }

		/// <summary>
		/// How many characters/numbers are allowed to be entered
		/// </summary>
		public int MaxLength { get; set; } = 10;

		public T Value
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
		public Func<TextEntryViewModel<T>, bool> ValueValidationAction { get; set; }

		#endregion


		#region Commands

		public RelayCommand SaveProjectCountCommnad { get; }
		public RelayCommand LostFocusCommand { get; set; }
		public RelayCommand RemoveFocusCommand { get; set; }

		#endregion


		public TextEntryViewModel(T value)
		{
			Value = value;
			_previousValue = value;


			SaveProjectCountCommnad = new RelayCommand(ExecuteSaveProjectCountCommnad);
			LostFocusCommand = new RelayCommand(ExecuteLostFocusCommand);
			RemoveFocusCommand = new RelayCommand(ExecuteRemoveFocusCommand);
		}

		public TextEntryViewModel() { }


		#region Command callbacks

		private void ExecuteLostFocusCommand()
		{
			// If focus was lost before changing the value
			// set current value to previous
			//Value.Equals(_previousValue);
			if(!Value.Equals(_previousValue))
			{
				Value = _previousValue;
			};
		}

		private void ExecuteSaveProjectCountCommnad()
		{
			// Validate setting value
			if(ValueValidationAction?.Invoke(this) == true)
			{
				// Update value
				_previousValue = Value;
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