namespace ProjectNotifier.XPlace.Core
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Timers;

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
        public event Action Notifications = () => { };


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
        /// Starts a background thread that will invoke <see cref="Notifications"/> every <see cref="Interval"/>
        /// </summary>
        public void StartAutoNotifing(CancellationToken cancellationToken = default)
        {
            // Don't run if already updating
            if (NotifierWorking == true)
                return;

            // If notification is null (Because it was cancelled)
            if (Notifications is null)
                // Re-Instantiate
                Notifications = () => { };


            // Set updating flag to true
            NotifierWorking = true;


            // Run a background task
            Task.Run(async () =>
            {
                // While updating flag is true
                while (NotifierWorking == true)
                {
                    // Wait for interval
                    await Task.Delay(Interval);

                    // Invoke event
                    Notifications?.Invoke();
                };
            }, cancellationToken);
        }


        /// <summary>
        /// Stops auto notifing
        /// </summary>
        public void StopAutoNotifing(bool removeNotifications = false)
        {
            // Disable notification event if requested
            if (removeNotifications)
                Notifications = null;

            // Set auto updating flag to false
            NotifierWorking = false;
        }
    };


    public class NotificationItem
    {
        private System.Timers.Timer _timer;


        /// <summary>
        /// Interval in milliseconds that indicates how often to invoke notification
        /// </summary>
        public int Interval { get; }

        /// <summary>
        /// A boolean flag that indicates if currently auto notifing
        /// </summary>
        public bool NotifierWorking { get; private set; }

        /// <summary>
        /// A name that the user can set for this timer
        /// </summary>
        public string TimerName { get; private set; }


        /// <summary>
        /// The event/notification that will be invoked
        /// </summary>
        public event Action Notification = () => { };



        public NotificationItem(int interval, Action notification, string timerName = "")
        {
            Interval = interval;
            TimerName = timerName;
            Notification += notification;

            _timer = new System.Timers.Timer(interval)
            {
                AutoReset = true,
            };

            _timer.Elapsed += ElapsedCallback;


            StartNotifing();
        }

       

        public void StartNotifing()
        {
            _timer.Start();

            NotifierWorking = true;
        }

        public void StopNotifing(bool removeNotification = false)
        {
            if (removeNotification == true)
                _timer.Elapsed -= ElapsedCallback;

            _timer.Stop();

            NotifierWorking = false;
        }

        private void ElapsedCallback(object sender, ElapsedEventArgs e)
        {
            Notification?.Invoke();
        }
    }
    


    public class Notifier
    {
        public List<NotificationItem> Notifications { get; private set; } = new List<NotificationItem>();

    };
};