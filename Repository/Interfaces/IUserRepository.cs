using Models;
using System.Data;

namespace Repository.Interfaces
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        void ValidateLogin(User user);
        string GetStoredHashedPassword(User user);
        List<Product> GetProducts();
        Product GetProductById(Product product);
        string ValidateAdminLogin(Admin admin);
        void StoreProduct(Product product);
        bool IsEmailExist(string email);
    }
}