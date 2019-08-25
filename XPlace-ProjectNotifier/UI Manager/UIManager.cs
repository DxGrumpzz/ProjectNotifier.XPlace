namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// The main UI manager for this app
	/// </summary>
	public class UIManager : IUIManager
	{
		/// <summary>
		/// Create a <see cref="ProjectNotificationView "/> notification and display it 
		/// </summary>
		/// <param name="projectNotificationViewModel"></param>
		/// <returns></returns>
		public void ShowProjectNotification(ProjectNotificationViewModel projectNotificationViewModel)
		{
			// Create instance of Project notification
			new ProjectNotificationView(projectNotificationViewModel)
			// show notification
			.Show();
		}
	};
};
