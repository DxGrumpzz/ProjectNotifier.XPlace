namespace XPlace_ProjectNotifier
{
	using System.Windows;


	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		public MainWindow(MainWindowViewModel mainWindowViewModel) :
			this()
		{
			DataContext = mainWindowViewModel;
		}
	}
}
