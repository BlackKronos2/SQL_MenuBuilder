using System.Windows;
using System.Windows.Controls;
using ModelViewContext;

namespace AccessControl
{
	/// <summary>
	/// Логика взаимодействия для AccessControlPage.xaml
	/// </summary>
	public partial class AccessControlPage : Page
	{
		public AccessControlPage() : base()
		{
			InitializeComponent();

			var modelView = DataContext as TableEditorViewModel;
			TableEditorPageManager manager = new TableEditorPageManager(AddButton, EditButton, DeleteButton, MainTable);
			manager.LoadModelView(modelView);
		}

		private void LogOutButton_Click(object sender, RoutedEventArgs e) => WindowsEvents.UserLogOut();

	}
}
