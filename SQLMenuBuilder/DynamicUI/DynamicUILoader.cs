using DatabaseManagement;
using ModelViewContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace SQLMenuBuilder
{
	public class CustomMenuItem : MenuItem
	{
		public ItemAccess Access { get; set; }
	}

	public class DynamicUILoader
	{
		private List<Item> _itemsList;

		public DynamicUILoader()
		{
			_itemsList = new List<Item>(0);
		}

		public void LoadItems(List<(MenuItemModel, MenuItemAccessModel)> items)
		{
			var itemDict = items.ToDictionary(item => item.Item1.Id, item => item.Item1);
			var itemAccess = items.ToDictionary(item => item.Item1.Id, item => (ItemAccess.LoadFromItem(item.Item2)));

			var mainItems = itemDict.Values.Where(i => i.ParentId == 0).ToList();
			var addedId = mainItems.Select(i => i.Id).ToList();

			foreach (var item in itemDict.Values.Where(i => i.ParentId == 0))
			{
				_itemsList.Add(new Item(item.Id, 0, item.Name, itemAccess[item.Id]));
			}

			while (_itemsList.Count != items.Count)
			{
				var childs = itemDict.Values.Where(i => addedId.Any(id => id == i.ParentId)).ToList();
				childs.RemoveAll(i => addedId.Contains(i.Id));
				childs = childs.OrderBy(i => i.ParentId).ToList();

				if (childs.Count == 0)
					break;

				foreach (var child in childs)
				{
					var parent = _itemsList.Find(i => i.Id == child.ParentId);
					_itemsList.Insert(_itemsList.IndexOf(parent) + 1, new Item(child.Id, parent.Level + 1, child.Name, itemAccess[child.Id], child.Key, child.DLL));
					addedId.Add(child.Id);
				}
			}
		}

		private CustomMenuItem ItemStatusUpdate(CustomMenuItem item)
		{
			if (!item.Access.HaveAccess)
				item.Visibility = Visibility.Hidden;

			if (!item.Access.Read)
				item.IsEnabled = false;

			return item;
		}

		private CustomMenuItem AddItem(CustomMenuItem menuItem, Item item)
		{
			CustomMenuItem newItem = new CustomMenuItem();
			newItem.Header = item.Name;
			newItem.Access = item.Access;
			newItem = ItemStatusUpdate(newItem);
			newItem.Command = new ViewModelCommand((object obj) => PagesLoader.LoadPage(item.DLL, item.Key));

			if (newItem.Visibility != Visibility.Hidden)
				menuItem.Items.Add(newItem);

			return newItem;
		}

		public List<CustomMenuItem> GetMenuItems()
		{
			List<CustomMenuItem> menuItems = new List<CustomMenuItem>(0);
			CustomMenuItem last_menu_item = new CustomMenuItem();
			int last_level = 0;
			for (int i = 0; i < _itemsList.Count; i++)
			{
				if (_itemsList[i].Level == 0)
				{
					last_menu_item = new CustomMenuItem();
					last_menu_item.Header = _itemsList[i].Name;
					last_menu_item.Access = _itemsList[i].Access;
					last_menu_item = ItemStatusUpdate(last_menu_item);

					menuItems.Add(last_menu_item);
					last_menu_item = menuItems[menuItems.Count - 1];
					last_level = _itemsList[i].Level;
				}
				else
				{
					for (int j = last_level; j > _itemsList[i].Level - 1; j--)
						last_menu_item = (CustomMenuItem)last_menu_item.Parent;
					if (_itemsList[i].Level != 1)
					{
						last_menu_item = AddItem(last_menu_item, _itemsList[i]);
						last_level = _itemsList[i].Level;
					}
					else
					{
						last_menu_item = menuItems[menuItems.Count - 1];
						last_menu_item = AddItem(last_menu_item, _itemsList[i]);
						last_level = _itemsList[i].Level;
					}
				}
			}
			var item_list = from CustomMenuItem item in menuItems
							where item.Visibility != Visibility.Hidden
							select item;

			return item_list.ToList();
		}
	}
}
