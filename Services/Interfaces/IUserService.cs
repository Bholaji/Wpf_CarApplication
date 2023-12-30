using Models;

namespace Services.Interfaces
{
    public interface IUserService
    {
        void SignUp(User user);
        bool LogIn(User user);
        List<Product> LoadProduct();
        Product LoadProductById(Product product);
        bool AdminLogIn(Admin admin);
        void StoreProductToDB(Product product);
        bool IsEmailExist(string email);
    }
}