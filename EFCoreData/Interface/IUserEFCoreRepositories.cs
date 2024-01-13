using Models;

namespace EFCoreData.Interface
{
    public interface IUserEFCoreRepositories
    {
        bool LoginUser(User user);
        void SignUpUser(User user);
        bool LoginAdminUser(User user);
        bool IsEmailExist(string email);
    }
}