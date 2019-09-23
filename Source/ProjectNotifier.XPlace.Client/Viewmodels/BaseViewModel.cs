namespace ProjectNotifier.XPlace.Client
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

	public class BaseViewModel : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Updates the UI when a property's value changes
		/// </summary>
		/// <param name="propertyName"> The name of the property </param>
		public void OnPropertyChanged([CallerMemberName]string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
