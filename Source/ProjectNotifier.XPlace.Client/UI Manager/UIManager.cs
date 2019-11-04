namespace ProjectNotifier.XPlace.Client
{
    using ProjectNotifier.XPlace.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

	/// <summary>
	/// The main UI manager for this app
	/// </summary>
	public class UIManager : IUIManager
	{
		/// <summary>
		/// Returns execution back to the main thread. To be used only for UI components
		/// </summary>
		/// <param name="action"> The function or code to execute </param>
		public void Dispatch(Action action)
		{
			// Invoke dispatcher and execute code
			Application.Current.Dispatcher.Invoke(action);
		}

		/// <summary>
		/// Create a <see cref="ProjectNotificationView "/> notification and display it 
		/// </summary>
		/// <param name="newProjects"></param>
		/// <returns></returns>
		public void ShowProjectNotification(IEnumerable<ProjectModel> newProjects)
		{
			Dispatch(() =>
			{
				// Quick dirty fix I hope(will) to improve later
				var projects = newProjects
				// "Convert" ProjectModels to an IEnumerable of ProjectNotificationItemViewModel
				.Select(project => 
				new ProjectNotificationItemViewModel()
				{
					ProjectModel = project,
				});


				// Create instance of Project notification
				new ProjectNotificationView(new ProjectNotificationViewModel()
				{
					NewProjectList = new List<ProjectNotificationItemViewModel>(projects),
				})
				// show notification
				.Show();
			});
		}
	};
};
