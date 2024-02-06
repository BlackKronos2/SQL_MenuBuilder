using DatabaseManagement;

namespace SQLMenuBuilder
{
	public struct ItemAccess
	{
		public bool Read { get; private set; }
		public bool Write { get; private set; }
		public bool Edit { get; private set; }
		public bool Delete { get; private set; }

		public ItemAccess(bool r, bool w, bool e, bool d)
		{
			Read = r;
			Write = w;
			Edit = e;
			Delete = d;
		}

		public static ItemAccess LoadFromItem(MenuItemAccessModel aM) => new ItemAccess(aM.Read > 0, aM.Write > 0, aM.Edit > 0, aM.Delete > 0);

		public bool HaveAccess => Read || Write || Edit || Delete;
	}

	public struct Item
	{
		private int _id;
		private int _level;

		public string Name { get; set; }
		public string Key { get; private set; }
		public int Level
		{
			get { return _level; }
			set { if (value >= 0) _level = value; }
		}
		public string DLL { get; private set; }
		public ItemAccess Access { get; private set; }

		public int Id => _id;

		public Item(int id, int level, string name, ItemAccess access)
		{
			_id = id;

			_level = level;
			Name = name;
			Access = access;

			Key = "";
			DLL = "";
		}
		public Item(int id, int level, string name, ItemAccess access, string key, string dll)
		{
			_id = id;

			_level = level;
			Name = name;
			Access = access;

			Key = key;
			DLL = dll;
		}

		public static bool operator !=(Item a, Item b) => a.Name != b.Name || a.DLL != b.DLL || a.Key != b.Key;
		public static bool operator ==(Item a, Item b) => a.Name == b.Name && a.DLL == b.DLL && a.Key == b.Key;
	}
}
