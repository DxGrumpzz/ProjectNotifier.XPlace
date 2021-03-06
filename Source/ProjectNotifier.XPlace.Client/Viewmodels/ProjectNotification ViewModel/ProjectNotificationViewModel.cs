﻿namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Media.Animation;

	/// <summary>
	/// 
	/// </summary>
	public class ProjectNotificationViewModel : BaseViewModel
	{

		public static ProjectNotificationViewModel Instance => new ProjectNotificationViewModel()
		{
			NewProjectList = new List<ProjectNotificationItemViewModel>()
			{
				new ProjectNotificationItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project1",
					}
				},

				new ProjectNotificationItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project2",
					}
				},

				new ProjectNotificationItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project3",
					}
				},

				new ProjectNotificationItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project4",
					}
				},

				new ProjectNotificationItemViewModel()
				{
					ProjectModel = new ProjectModel()
					{
						Title = "Project5",
					}
				},
			},
		};



		#region Private fields

		private Window _window;

		private AppSettingsDataModel _settings;

		#endregion


		#region Public properties

		/// <summary>
		/// The width of the window
		/// </summary>
		public double Width => 400d;

		/// <summary>
		/// The height of the window
		/// </summary>
		public double Height => 200d;


		/// <summary>
		/// Window Y starting position
		/// </summary>
		public double Top => (SystemParameters.WorkArea.BottomLeft.Y - Height) - 1;



		/// <summary>
		/// The list of new projects
		/// </summary>
		public List<ProjectNotificationItemViewModel> NewProjectList { get; set; }


		#endregion


		#region Commands

		public RelayCommand CloseWindowCommand { get; }

		#endregion



		public ProjectNotificationViewModel()
		{
			_settings = DI.ClientAppSettings();


            CloseWindowCommand = new RelayCommand(async () =>
			{
				// Animate window closing
				await AnimateOut(TimeSpan.FromSeconds(0.2), (sender, e) =>
				{
					// After animation finishes, actually close the window
					_window.Close();
				});
			});
		}


		/// <summary>
		/// Binds a window to this viewmodel's window reference
		/// </summary>
		/// <param name="window"></param>
		public void BindWindow(Window window)
		{
			// Bind window
			_window = window;

			// hook events
			window.Loaded += Window_Loaded;
		}


		#region Event callbacks

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Animate window opening
			await AnimateIn(TimeSpan.FromSeconds(0.2));


			// Wait a bit for the user to read the new projects
			await Task.Delay(_settings.KeepNotificationOpenSeconds * 1000);

			// Animate window closing
			await AnimateOut(TimeSpan.FromSeconds(0.2),
			(sender, e) =>
			{
				// After animation finishes, actually close the window
				_window.Close();
			});
		}


		#endregion


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
			DoubleAnimation doubleAnimation = new DoubleAnimation(1920, 1920 - Width, duration);

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
			DoubleAnimation doubleAnimation = new DoubleAnimation(1920 - Width, 1920, duration);

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