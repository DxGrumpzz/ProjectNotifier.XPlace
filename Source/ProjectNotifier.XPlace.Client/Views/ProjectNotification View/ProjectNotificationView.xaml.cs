namespace ProjectNotifier.XPlace.Client
{
	using System.Windows;

	/// <summary>
	/// Interaction logic for ProjectNotificationView.xaml
	/// </summary>
	public partial class ProjectNotificationView : Window
	{

		public ProjectNotificationView()
		{
			DataContext = new ProjectNotificationViewModel();
			((ProjectNotificationViewModel)DataContext).BindWindow(this);

			InitializeComponent();
		}

		public ProjectNotificationView(ProjectNotificationViewModel viewModel)
		{
			DataContext = viewModel;
			viewModel.BindWindow(this);

			InitializeComponent();
		}
	};
};
