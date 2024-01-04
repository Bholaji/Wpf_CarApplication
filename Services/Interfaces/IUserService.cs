using Models;

namespace Services.Interfaces
{
    public interface IUserService
    {
        void SignUp(User user);
        bool LogIn(User user);
        bool AdminLogIn(User user);
        bool IsEmailExist(string email);
    }
}