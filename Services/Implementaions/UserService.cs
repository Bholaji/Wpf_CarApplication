using Models;
using Repository.Implementations;
using Services.Interfaces;
using Services.Utilities;
using System.Security.Cryptography;
using System.Text;
using Repository.Interfaces;
using serviceUtility = Services.Utilities.Utilities;

namespace Services.Implementaions
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private IUtilities utilities;

        public UserService()
        {
            utilities = new serviceUtility();
            userRepository = new UserRepository();
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


    }
}