namespace XPlace_ProjectNotifier
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// An interface for a UI manager. Responsible for showing notifications and such
	/// </summary>
	public interface IUIManager
	{
		/// <summary>
		/// Returns execution back to the main thread. To be used only for UI components
		/// </summary>
		/// <param name="action"> The function or code to execute </param>
		public void Dispatch(Action action);


		/// <summary>
		/// Create a <see cref="ProjectNotificationView "/> notification and display it
		/// </summary>
		/// <param name="projectNotificationViewModel"></param>
		/// <returns></returns>
		public void ShowProjectNotification(List<ProjectNotificationItemViewModel> newProjectsList);

	};
}
