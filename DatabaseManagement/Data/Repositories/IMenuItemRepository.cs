
namespace DatabaseManagement
{
	public interface IMenuItemRepository
	{
		void Add(MenuItemModel menuItem);
		void Edit(MenuItemModel menuItem);
		void Delete(MenuItemModel menuItem);

	}
}
