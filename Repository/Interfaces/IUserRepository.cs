using Models;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void ValidateLogin(User user);
        string GetStoredHashedPassword(User user);
    }
}