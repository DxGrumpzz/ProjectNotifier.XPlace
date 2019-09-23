namespace ProjectNotifier.XPlace.Core
{
	using System;

	/// <summary>
	/// A class that represents a Project from XPlace
	/// </summary>
	public class ProjectModel
	{
		/// <summary>
		/// The proejct's title
		/// </summary>
		public string Title { get; set; }
	
		/// <summary>
		/// The proejct's URL
		/// </summary>
		public string Link { get; set; }
	
		/// <summary>
		/// The proejct's description
		/// </summary>
		public string Description { get; set; }
	
		/// <summary>
		/// The proejct's publishing date
		/// </summary>
		public DateTime PublishingDate { get; set; }

		/// <summary>
		/// A unique ID number for this project
		/// </summary>
		public int ProjectID { get; set; }
	};
}
