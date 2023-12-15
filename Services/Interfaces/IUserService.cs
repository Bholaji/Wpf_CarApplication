using Models;

namespace Services.Interfaces
{
    public interface IUserService
    {
        void SignUp(User user);
        bool LogIn(User user);
    }
}