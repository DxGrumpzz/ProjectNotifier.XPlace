namespace XPlace_ProjectNotifier
{
	using System;
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
		/// <param name="projectNotificationViewModel"></param>
		/// <returns></returns>
		public void ShowProjectNotification(ProjectNotificationViewModel projectNotificationViewModel)
		{
			Dispatch(() =>
			{
				// Create instance of Project notification
				new ProjectNotificationView(projectNotificationViewModel)
				// show notification
				.Show();
			});
		}
	};
};
