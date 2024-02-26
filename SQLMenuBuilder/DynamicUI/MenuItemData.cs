using DatabaseManagement;

namespace SQLMenuBuilder
{
	public struct PageAccess
	{
		public bool Read { get; private set; }
		public bool Write { get; private set; }
		public bool Edit { get; private set; }
		public bool Delete { get; private set; }

		public PageAccess(bool r, bool w, bool e, bool d)
		{
			Read = r;
			Write = w;
			Edit = e;
			Delete = d;
		}

		public static PageAccess LoadFromItem(MenuPageAccessModel aM) => new PageAccess(aM.Read > 0, aM.Write > 0, aM.Edit > 0, aM.Delete > 0);

		public bool HaveAccess => Read || Write || Edit || Delete;
	}
}
