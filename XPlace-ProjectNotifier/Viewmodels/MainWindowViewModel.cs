namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	public class MainWindowViewModel : BaseViewModel
	{
		private MainWindowModel _model;
		private string _newTitle;


		public MainWindowModel Model
		{
			get => _model;
			set
			{
				_model = value;
				OnPoropertyChanged();
			}
		}


		public string NewTitle
		{
			get => _newTitle; 
			set
			{
				_newTitle = value;
				OnPoropertyChanged();
			}
		}


		public RelayCommand Command { get; }

		public MainWindowViewModel()
		{
			Command = new RelayCommand(ExecuteCommand);
		}


		private void ExecuteCommand()
		{
			Model = new MainWindowModel()
			{
				Title = NewTitle,
			};
		}
	}
}
