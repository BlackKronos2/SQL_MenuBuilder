using ModelViewContext;
using System.Windows;
using System.Windows.Controls;

namespace TestWindows
{
	/// <summary>
	/// Логика взаимодействия для Page3.xaml
	/// </summary>
	public partial class Page3 : Page
	{
		public Page3()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e) => WindowsEvents.UserLogOut();

	}
}
