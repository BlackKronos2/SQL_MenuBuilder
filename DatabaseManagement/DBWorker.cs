using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace DatabaseManagement
{
	public class DBWorker : DbContext, IUserRepository, IMenuItemRepository
	{
		#region SINGLETON

		private static DBWorker _instance;
		public static DBWorker GetInstance()
		{
			if (_instance == null)
				_instance = new DBWorker();
			return _instance;
		}

		#endregion

		private DbSet<UserModel> Users { get; set; }
		private DbSet<MenuItemModel> MenuItems { get; set; }
		private DbSet<MenuItemAccessModel> AccessList { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			string connectString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

			connectString = baseDirectory + "\\" + connectString;

			optionsBuilder.UseSqlite($"Data Source={connectString}");
			optionsBuilder.UseLazyLoadingProxies();
		}
		public DBWorker() : base()
		{ }


		#region MODEL_CREATE

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<MenuItemAccessModel>()
				.HasOne(ma => ma.MenuItem)
				.WithMany()
				.HasForeignKey(ma => ma.MenuId);
			modelBuilder.Entity<MenuItemAccessModel>()
				.HasOne(access => access.User)
				.WithMany()
				.HasForeignKey(access => access.UserId);
		}

		#endregion

		#region USERS

		public List<UserModel> GetUsersList() => Users.ToList();

		public void Add(UserModel user)
		{
			if (Users.Any(u => u.Login == user.Login))
				throw new Exception($"Пользователь с логином {user.Login} уже существует");

			if (user.Login.Length < 4)
				throw new Exception("Логин слишком короткий");

			if (user.Password.Length < 5)
				throw new Exception("Пароль слишком короткий");

			Users.Add(user);
			SaveChanges();
		}
		public void Delete(UserModel user)
		{
			if (!Users.Any(u => u.Id == user.Id))
				throw new Exception("Данного пользователя не существует");

			Users.Remove(user);
			SaveChanges();
		}

		public UserModel GetById(int id)
		{
			throw new NotImplementedException();
		}
		public UserModel GetByLogin(string login)
		{
			if (!Users.Any(u => u.Login == login))
				throw new Exception($"Пользователь {login} не существует");

			var user = Users.First(u => u.Login == login);
			return user;
		}

		public bool Authorization(string login, string password)
		{
			if (!Users.Any(u => u.Login == login && u.Password == password))
				throw new Exception("Неверный логин или пароль");

			return true;
		}

		#endregion

		#region MENU_ITEMS

		public List<MenuItemModel> GetMenuList() => MenuItems.ToList();

		public void Add(MenuItemModel menuItem)
		{
			throw new NotImplementedException();
		}

		public void Delete(MenuItemModel menuItem)
		{
			throw new NotImplementedException();
		}

		public void Edit(MenuItemModel menuItem)
		{
			throw new NotImplementedException();
		}

		public List<MenuItemAccessModel> LoadItems(UserModel user)
		{
			var userAcess = AccessList.Where(a => a.UserId == user.Id).ToList();
			return userAcess;
		}


		#endregion

		#region ACCESS

		public List<MenuItemAccessModel> GetMenuAccessList() => AccessList.ToList();

		public void Add(MenuItemAccessModel menuItemAccessModel)
		{
			if (AccessList.Any(access => access.MenuId == menuItemAccessModel.MenuId && access.UserId == menuItemAccessModel.UserId))
				throw new Exception("Подобный доступ уже открыт");

			AccessList.Add(menuItemAccessModel);
			SaveChanges();
		}

		public void Edit(MenuItemAccessModel menuItemAccessModel)
		{
			var accessToEdit = AccessList.Find(menuItemAccessModel.Id);

			if (accessToEdit == null)
				throw new Exception("Данные для редактирования не найдены");

			Entry(accessToEdit).CurrentValues.SetValues(menuItemAccessModel);
			SaveChanges();
			
		}

		public void Delete(MenuItemAccessModel menuItemAccessModel)
		{
			var accessToDelete = AccessList.Find(menuItemAccessModel.Id);

			if(accessToDelete == null)
				throw new Exception("Данные для редактирования не найдены");

			AccessList.Remove(accessToDelete);
			SaveChanges();
		}

		#endregion

	}
}
