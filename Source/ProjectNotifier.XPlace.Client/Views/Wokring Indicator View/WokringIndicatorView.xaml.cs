namespace ProjectNotifier.XPlace.Client
{
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for WokringIndicatorView.xaml
    /// </summary>
    public partial class WorkingIndicatorView : UserControl
    {
        public WorkingIndicatorView()
        {
            InitializeComponent();
        }


        /// <summary>
        /// A boolean flag that indicates if the working/loding indicator is shown 
        /// </summary>
        public bool Working
        {
            get => (bool)GetValue(WorkingProperty);
            set => SetValue(WorkingProperty, value);
        }

        public static readonly DependencyProperty WorkingProperty =
            DependencyProperty.Register(
                nameof(Working),
                typeof(bool),
                typeof(WorkingIndicatorView),
                new FrameworkPropertyMetadata(false));
    };
};
