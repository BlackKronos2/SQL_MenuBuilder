using DatabaseManagement;
using ModelViewContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SQLMenuBuilder
{
	public class MainWindowModelView : ViewModelBase
	{
		private Visibility _windowVisibility = Visibility.Visible;

		public Visibility WindowVisibility
		{
			get { return _windowVisibility; }
			set
			{
				_windowVisibility = value;
				OnPropertyChanged(nameof(WindowVisibility));
			}
		}

		public MainWindowModelView()
		{ }

		public List<CustomMenuItem> LoadMenu()
		{
			try
			{
				string login = Thread.CurrentPrincipal.Identity.Name;

				var user = DBWorker.GetInstance().GetByLogin(login);
				var items = DBWorker.GetInstance().LoadItems(user);

				MenuBuilder dynamicUI = new MenuBuilder();
				return dynamicUI.GetMenuItems(items);
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
				return new List<CustomMenuItem>(0);
			}
		}
	}
}
