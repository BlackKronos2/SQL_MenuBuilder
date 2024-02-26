
namespace DatabaseManagement
{
	public class MenuPageAccessModel : DataModel
	{
		public int UserId { get; set; }
		public int MenuId { get; set; }


		public int Read { get; set; }
		public int Write { get; set; }
		public int Edit { get; set; }
		public int Delete { get; set; }

		public virtual UserModel User { get; set; }
		public virtual MenuItemModel MenuItem { get; set; }
	}
}
