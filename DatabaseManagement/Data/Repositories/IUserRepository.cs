
namespace DatabaseManagement
{
	public interface IUserRepository
	{
		void Add(UserModel user);
		void Delete(UserModel user);
		UserModel GetById(int id);
		UserModel GetByLogin(string login);
		bool Authorization(string login, string password);
	}
}
