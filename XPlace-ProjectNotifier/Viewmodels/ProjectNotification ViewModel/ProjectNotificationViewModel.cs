namespace XPlace_ProjectNotifier
{
	using System;
	using System.ComponentModel;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Media.Animation;

	/// <summary>
	/// 
	/// </summary>
	public class ProjectNotificationViewModel : BaseViewModel
	{

		#region Private fields

		private Window _window;

		#endregion


		#region Public properties

		/// <summary>
		/// The width of the window
		/// </summary>
		public double Width => 300d;

		/// <summary>
		/// The height of the window
		/// </summary>
		public double Height => 150d;

		#endregion

		#region Commands

		public RelayCommand CloseWindowCommand { get; }

		#endregion

		public ProjectNotificationViewModel() :
			this(new ProjectNotificationView())
		{
		}

		public ProjectNotificationViewModel(ProjectNotificationView window)
		{
			_window = window;


			window.Loaded += Window_Loaded;

			CloseWindowCommand = new RelayCommand(() =>
			{
				window.Close();
			});
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Animate window opening
			await AnimateIn(TimeSpan.FromSeconds(0.2));

			// Wait abit for the user to read the message
			await Task.Delay(3000);

			await Close();
		}


		public void Show()
		{
			_window.Show();
		}

		public async Task Close()
		{
			// Animate window closing
			await AnimateOut(TimeSpan.FromSeconds(0.2), (sender, e) =>
			{
				_window.Close();
			});
		}


		#region Private helpers

		/// <summary>
		/// Animates a window sliding in from the outside of the screen
		/// </summary>
		/// <param name="duration"> How long until the animation completes </param>
		/// <returns></returns>
		private async Task AnimateIn(TimeSpan duration)
		{
			// Create a storybaord
			Storyboard storyboard = new Storyboard();

			// The animation 
			DoubleAnimation doubleAnimation = new DoubleAnimation(2220, 1000, duration);

			// The property to animate
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(nameof(Window.Left)));

			// Add animation to storyboard 
			storyboard.Children.Add(doubleAnimation);

			// Animate
			storyboard.Begin(_window);

			// Wait until animation completes
			await Task.Delay(duration);
		}

		/// <summary>
		/// Animates a window sliding out outside of the screen
		/// </summary>
		/// <param name="duration"> How long until the animation completes </param>
		/// <returns></returns>
		private async Task AnimateOut(TimeSpan duration, EventHandler completedEvent)
		{
			// Create a storybaord
			Storyboard storyboard = new Storyboard();

			// Bind completed event
			storyboard.Completed += completedEvent;

			// The animation 
			DoubleAnimation doubleAnimation = new DoubleAnimation(1000, 2220, duration);

			// The property to animate
			Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(nameof(Window.Left)));

			// Add animation to storyboard 
			storyboard.Children.Add(doubleAnimation);

			// Animate
			storyboard.Begin(_window);

			// Wait until animation completes
			await Task.Delay(duration);
		}

		#endregion
	};
};
