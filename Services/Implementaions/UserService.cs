using Models;
using Repository.Implementations;
using Services.Interfaces;
using Services.Utilities;
using System.Security.Cryptography;
using System.Text;
using Repository.Interfaces;
using serviceUtility = Services.Utilities.Utilities;
using System.Globalization;

namespace Services.Implementaions
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IUtilities utilities;
        private List<Product> ProductList;

        public UserService()
        {
            utilities = new serviceUtility();
            userRepository = new UserRepository();
            ProductList = new List<Product>();
        }

        public void SignUp(User user)
        {
            try
            {
               user.Password = utilities.HashPassword(user.Password);
                userRepository.CreateUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"User signup failed. Please try again {ex.Message}");
            }
            
        }

        public bool LogIn(User user)
        {
            try
            {
                string storedHash = userRepository.GetStoredHashedPassword( user);
                bool isPasswordcorrect = utilities.VerifyPassword(user.Password,storedHash);
                if (isPasswordcorrect)
                {
                    return true;
                    
                }
                else 
                {
                    return false;
                }

            }
            catch(Exception ex)
            {
                throw new Exception($" {ex.Message}");
            }
            
        }

        public bool AdminLogIn(Admin admin)
        {
            try {
                string receivedPassword = userRepository.ValidateAdminLogin(admin);
                bool isAdmin = utilities.VerifyNormalPassword(admin.AdminPassword, receivedPassword);

                if (isAdmin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Product> LoadProduct()
        {
            List<Product> loadedProducts = userRepository.GetProducts();

            ProductList = loadedProducts;

            return ProductList;
        }

        public Product LoadProductById(Product product)
        {
            try
            {
                return userRepository.GetProductById(product);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void StoreProductToDB(Product product)
        {
            try
            {
                userRepository.StoreProduct(product);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                return userRepository.IsEmailExist(email);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}