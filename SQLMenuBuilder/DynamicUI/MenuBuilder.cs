using DatabaseManagement;
using ModelViewContext;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace SQLMenuBuilder
{
	/// <summary>
	/// Элемент управления с учетом доступа
	/// </summary>
	public class CustomMenuItem : MenuItem
	{
		public PageAccess Access { get; set; }
	}

	public class MenuBuilder
	{

		public MenuBuilder()
		{ }

		/// <summary>
		/// Формирование доступа к меню
		/// </summary>
		/// <param name="item"> Информация о создаваемом пункте меню </param>
		/// <returns></returns>
		private CustomMenuItem ItemStatusUpdate(CustomMenuItem item)
		{
			if (!item.Access.HaveAccess)
				item.Visibility = Visibility.Hidden;

			if (!item.Access.Read)
				item.IsEnabled = false;

			return item;
		}

		/// <summary>
		/// Добавление дочернего пункта к родительскому
		/// </summary>
		/// <param name="menuItem"> Родительский пункт меню </param>
		/// <param name="model"> Информация о создаваемом пункте меню </param>
		/// <returns></returns>
		private CustomMenuItem AddItem(CustomMenuItem menuItem, MenuPageAccessModel model)
		{
			CustomMenuItem newItem = new CustomMenuItem();
			newItem.Header = model.MenuItem.Name;
			PageAccess pageAccess = PageAccess.LoadFromItem(model);
			newItem.Access = pageAccess;
			newItem = ItemStatusUpdate(newItem);
			newItem.Command = new ViewModelCommand((object obj) => PagesLoader.LoadPage(model.MenuItem.DLL, model.MenuItem.Key, pageAccess));

			if (newItem.Visibility != Visibility.Hidden)
				menuItem.Items.Add(newItem);

			return newItem;
		}

		/// <summary>
		/// Загрузка итогового контекстого меню пользователя
		/// </summary>
		/// <param name="items"> List модели доступа </param>
		/// <returns></returns>
		public List<CustomMenuItem> GetMenuItems(List<MenuPageAccessModel> items)
		{
			List<CustomMenuItem> menuItems = new List<CustomMenuItem>(0); // Все пункты меню
			Dictionary<int, CustomMenuItem> menuDict = new Dictionary<int, CustomMenuItem>(); //Словать для доступа к пунктам меню по Id пункта меню из БД

			// Меню на самом верхнем уровне
			var mainItems = items.Where(i => i.MenuItem.ParentId == 0).ToList();

			foreach (var item in mainItems)
			{
				CustomMenuItem menuItem = new CustomMenuItem();
				menuItem.Header = item.MenuItem.Name;
				PageAccess pageAccess = PageAccess.LoadFromItem(item);
				menuItem.Access = pageAccess;
				menuItem.Command = new ViewModelCommand((object obj) => PagesLoader.LoadPage(item.MenuItem.DLL, item.MenuItem.Key, pageAccess));

				menuItem = ItemStatusUpdate(menuItem);
				menuItems.Add(menuItem);
				menuDict.Add(item.MenuId, menuItem);
			}

			// По циклу присоединяем дочерние пункты меню к родительским
			while (menuItems.Count != items.Count)
			{
				var childs = items.Where(i => menuDict.Keys.Any(id => id == i.MenuItem.ParentId)).ToList();
				childs.RemoveAll(i => menuDict.Keys.Contains(i.MenuId));
				childs = childs.OrderBy(i => i.MenuItem.ParentId).ToList();

				if (childs.Count == 0)
					break;

				foreach (var child in childs)
				{
					var parent = menuDict[child.MenuItem.ParentId];

					var childItem = AddItem(parent, child);
					menuDict.Add(child.MenuId, childItem);
				}
			}

			return menuItems;
		}
	}
}
