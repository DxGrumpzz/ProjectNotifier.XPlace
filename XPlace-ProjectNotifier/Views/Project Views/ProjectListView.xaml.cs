namespace XPlace_ProjectNotifier
{
	using System.Windows.Controls;

	/// <summary>
	/// Interaction logic for ProjectListView.xaml
	/// </summary>
	public partial class ProjectListView : UserControl
	{
		public ProjectListView()
		{
			InitializeComponent();
		}

		public ProjectListView(ProjectListViewModel viewModel) :
			this()
		{
			DataContext = viewModel;
		}
	};
}
