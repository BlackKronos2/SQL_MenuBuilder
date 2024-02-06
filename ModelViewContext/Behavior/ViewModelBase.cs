using System.ComponentModel;
using System.Windows;

namespace ModelViewContext
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void ErrorMessage(string message) => MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		protected void SuccessMessage(string message) => MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Information);

		public void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public ViewModelBase()
		{

		}
	}
}
