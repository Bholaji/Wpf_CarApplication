using Models;
using System.Data;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        bool ValidateLogin(User user);
        string GetStoredHashedPassword(User user);
        bool ValidateAdminLogin(User user);
        bool IsEmailExist(string email);
    }
}