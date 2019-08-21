namespace XPlace_ProjectNotifier
{
	using System;
	using System.Linq;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.Threading.Tasks;

	public class MainWindowViewModel : BaseViewModel
	{

		#region Private fields

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private MainWindowModel _model;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool _isLoading = true;

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ProjectListViewModel projectListViewModel;

		#endregion


		#region Public properties

		public MainWindowModel Model
		{
			get => _model;
			set
			{
				_model = value;
				OnPropertyChanged();
			}
		}

		public ProjectListViewModel ProjectList
		{
			get => projectListViewModel;
			set
			{
				projectListViewModel = value;
				OnPropertyChanged();

			}
		}

		public SettingsModel SettingsModel { get; }

		public SettingsViewModel SettingsViewModel { get; }

		public ProjectLoader ProjectLoader { get; }


		public bool IsLoading
		{
			get => _isLoading;
			private set
			{
				_isLoading = value;
				OnPropertyChanged();
			}
		}

		#endregion


		#region Commands

		public RelayCommand OpenSettingsCommand { get; }

		#endregion



		public MainWindowViewModel() { }

		public MainWindowViewModel(SettingsModel settingsModel, ProjectLoader projectLoader)
		{
			SettingsModel = settingsModel;
			SettingsViewModel = new SettingsViewModel(settingsModel);
			ProjectLoader = projectLoader;


			Task.Run(SetupRSSProjectListAsync);
			ProjectLoader.StartAutoUpdating();


			OpenSettingsCommand = new RelayCommand(() =>
			{
				SettingsViewModel.IsOpen = true;
			});

			// When project count setting is saved...
			SettingsViewModel.ProjectCountSetting.SaveChangesAction += new Action<TextEntryViewModel<int>>(async (value) =>
			{
				// Reset project list
				ProjectList.ProjectList = new ObservableCollection<ProjectItemViewModel>();

				// Display loading text
				IsLoading = true;

				// Load new project list
				await SetupRSSProjectListAsync();
			});

		}


		#region Private methods

		/// <summary>
		/// Asynchrounsly loads rss feed content
		/// </summary>
		/// <returns></returns>
		private async Task SetupRSSProjectListAsync()
		{
			// Load projects
			await Task.Run(() => ProjectList = DI.GetService<ProjectLoader>().LoadProjects());

			// Finished loading content
			IsLoading = false;
		}

		#endregion

	}
}