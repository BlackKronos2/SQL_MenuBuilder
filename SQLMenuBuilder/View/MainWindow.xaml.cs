using ModelViewContext;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace SQLMenuBuilder
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			LoadMenu();
			WindowsEvents.OnLoadPage += LoadPage;
			WindowsEvents.OnLogOut += () => this.Hide();
			WindowsEvents.OnLogOut += UnsubscribeFromEvents;
		}

		private void LoadMenu()
		{
			var modelView = DataContext as MainWindowModelView;
			List<CustomMenuItem> list = modelView.LoadMenu();

			foreach (MenuItem item in list)
				MainMenu.Items.Add(item);
		}

		private void LoadPage(Page page)
		{
			ContentControl contentControl = new ContentControl();
			contentControl.Content = page.Content;
			contentControl.HorizontalAlignment = HorizontalAlignment.Stretch;
			contentControl.VerticalAlignment = VerticalAlignment.Stretch; 

			StackPanel stackPanel = new StackPanel();
			DockPanel.SetDock(contentControl, Dock.Top);

			if (MainMenu.Parent is StackPanel panel)
				panel.Children.Remove(MainMenu);

			MainGrid.Children.Remove(MainMenu);

			stackPanel.Children.Add(MainMenu);
			stackPanel.Children.Add(contentControl);

			this.Content = stackPanel;
		}

		private void UnsubscribeFromEvents()
		{
			WindowsEvents.OnLogOut -= () => this.Hide();
			WindowsEvents.OnLoadPage -= LoadPage;
		}

		private void Window_Closed(object sender, System.EventArgs e)
		{
			UnsubscribeFromEvents();
			WindowsEvents.UserLogOut();
		}
	}
}
