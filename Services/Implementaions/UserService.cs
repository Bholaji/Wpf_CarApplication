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

        public UserService()
        {
            utilities = new serviceUtility();
            userRepository = new UserRepository();
        }

        public void SignUp(User user)
        {
            try
            {
                user.Role = Role.Regular;
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
                /*string storedHash = userRepository.GetStoredHashedPassword( user);
                bool isPasswordcorrect = utilities.VerifyPassword(user.Password,storedHash);*/
                user.Password = utilities.HashPassword(user.Password);
                bool isPasswordcorrect = userRepository.ValidateLogin(user);
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

        public bool AdminLogIn(User user)
        {
            bool isAdmin = userRepository.ValidateAdminLogin(user);
            if (isAdmin)
            {
                return true;
            }
            else
            {
                return false;
            }
            /*try
            {
                string receivedPassword = userRepository.ValidateAdminLogin(user);
                bool isAdmin = utilities.VerifyNormalPassword(user.Password, receivedPassword);

                if (isAdmin)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }*/
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