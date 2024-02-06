using ModelViewContext;
using System.Windows;
using System.Windows.Controls;

namespace TestWindows
{
	/// <summary>
	/// Логика взаимодействия для Page1.xaml
	/// </summary>
	public partial class Page1 : Page
	{
		public Page1()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e) => WindowsEvents.UserLogOut();

	}
}
