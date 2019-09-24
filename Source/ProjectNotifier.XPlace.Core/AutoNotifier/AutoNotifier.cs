namespace ProjectNotifier.XPlace.Client
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;

	/// <summary>
	/// A class that invokes an action every so often
	/// </summary>
	public class AutoNotifier
	{

		#region Public properties

		/// <summary>
		/// Interval in milliseconds that indicates how often to invoke notification
		/// </summary>
		public int Interval { get; }

		/// <summary>
		/// A boolean flag that indicates if currently auto notifing
		/// </summary>
		public bool NotifierWorking { get; private set; }

		#endregion


		#region Events

		/// <summary>
		/// The event/notification that will be invoked
		/// </summary>
		public event Action Notification = () => { };


		#endregion


		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="intervalInMS"> A set period where an event will be fired </param>
		public AutoNotifier(int intervalInMS = 5000)
		{
			Interval = intervalInMS;
		}


		/// <summary>
		/// Starts a background thread that will invoke <see cref="Notification"/> every <see cref="Interval"/>
		/// </summary>
		public void StartAutoNotifing(CancellationToken cancellationToken = default)
		{
			// Don't run if already updating
			if(NotifierWorking == true)
				return;

			// If notification is null (Because it was cancelled)
			if(Notification is null)
				// Re-Instantiate
				Notification = () => { };
			

			// Set updating flag to true
			NotifierWorking = true;


			// Run a background task
			Task.Run(async () =>
			{
				// While updating flag is true
				while(NotifierWorking == true)
				{
					// Wait for interval
					await Task.Delay(Interval);

					// Invoke event
					Notification?.Invoke();
				};
			}, cancellationToken);
		}


		/// <summary>
		/// Stops auto notifing
		/// </summary>
		public void StopAutoNotifing()
		{
			// Disable notification event
			Notification = null;

			// Set auto updating flag to false
			NotifierWorking = false;
		}
	};
};