using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManagement;
using Microsoft.EntityFrameworkCore;
using ModelViewContext;

namespace AccessControl
{
	public class AccessItem : MenuPageAccessModel
	{ 
		public string MenuName { get; set; }
		public string UserLogin { get; set; }
	}

	public class AccessControlPageModelView : TableEditorViewModel
	{

		public AccessControlPageModelView() : base()
		{
			LoadTable();
		}

		protected override void Add(object obj)
		{
			try
			{
				var modelView = new AccessEditModelView();
				WindowsEvents.OpenWindow(typeof(AccessEditWindow), modelView);
				LoadTable();
			}
			catch (Exception ex)
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Edit(object obj)
		{
			try
			{
				base.Edit(obj);

				var item = Database.GetMenuAccessList().Find(a => a.Id == SelectedItem.Id);
				var modelView = new AccessEditModelView(item);
				WindowsEvents.OpenWindow(typeof(AccessEditWindow), modelView);
				LoadTable();
			}
			catch (Exception ex) 
			{
				ErrorMessage(ex.Message);
			}
		}

		protected override void Delete(object obj)
		{
			try
			{
				base.Delete(obj);

				var item = Database.GetMenuAccessList().Find(a => a.Id == SelectedItem.Id);
				Database.Delete(item);
				SuccessMessage("Успешное удаление");
				LoadTable();
			}
			catch(Exception ex) 
			{
				ErrorMessage(ex.Message);
			}
		}

		private void LoadTable()
		{
			List<AccessItem> accessItems = new List<AccessItem>(0);

			var accessList = Database.GetMenuAccessList();

			foreach (var access in accessList)
			{
				accessItems.Add(new AccessItem()
				{
					Id = access.Id,
					MenuName = access.MenuItem.Name,
					UserLogin = access.User.Login,
					Read = access.Read,
					Write = access.Write,
					Edit = access.Edit,
					Delete = access.Delete,
				});
			}

			Items = new ObservableCollection<DataModel>(accessItems);
			SelectedItem = null;
		}

	}
}
