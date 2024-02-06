using ModelViewContext;
using System.Windows;
using System.Windows.Controls;

namespace TestWindows
{
	/// <summary>
	/// Логика взаимодействия для Page2.xaml
	/// </summary>
	public partial class Page2 : Page
	{
		public Page2()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e) => WindowsEvents.UserLogOut();

	}
}
