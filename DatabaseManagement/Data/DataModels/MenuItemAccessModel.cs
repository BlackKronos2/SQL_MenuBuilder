
namespace DatabaseManagement
{
	public class MenuItemAccessModel : DataModel
	{
		public int UserId { get; set; }
		public int MenuId { get; set; }


		public int Read { get; set; }
		public int Write { get; set; }
		public int Edit { get; set; }
		public int Delete { get; set; }

		public UserModel User { get; set; }
		public MenuItemModel MenuItem { get; set; }
	}
}
