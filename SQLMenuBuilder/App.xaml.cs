using ModelViewContext;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace SQLMenuBuilder
{
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void OpenWindow(Type windowType, ViewModelBase viewModel)
		{
			Assembly assembly = windowType.Assembly;
			Type type = assembly.GetType(windowType.FullName);

			//try
			//{
				Window window = (Window)Activator.CreateInstance(type);
				window.DataContext = viewModel;
				window.ShowDialog();
			//}
			//catch (Exception ex) 
			//{
				
			//}
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			WindowsEvents.OnLogin += () => (new MainWindow()).Show();
			WindowsEvents.OnLogOut += () => (new LoginWindow()).Show();
			WindowsEvents.OnOpenWindow += OpenWindow;		
		}
	}
}
