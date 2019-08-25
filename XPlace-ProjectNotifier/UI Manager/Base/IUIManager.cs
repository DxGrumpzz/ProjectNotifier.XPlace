namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// An interface for a UI manager. Responsible for showing notifications and such
	/// </summary>
	public interface IUIManager
	{
		/// <summary>
		/// Create a <see cref="ProjectNotificationView "/> notification and display it
		/// </summary>
		/// <param name="projectNotificationViewModel"></param>
		/// <returns></returns>
		public ProjectNotificationView ShowProjectNotification(ProjectNotificationViewModel projectNotificationViewModel);

	};
}
