using EFCoreData.Implementations;
using EFCoreData.Interface;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util = Services.Utilities.Utilities;

namespace Services.Implementaions
{
    public class UserServiceEFCore : IUserEFCoreRepositories
    {
        private readonly RepositoryDB _dbContext;
        private readonly IUtilities utilities;
        private readonly IUserEFCoreRepositories eFCoreRepositories;

        public UserServiceEFCore()
        {
            _dbContext = new RepositoryDB();
            utilities = new Util();
            eFCoreRepositories = new UserEFCoreRepositories(_dbContext);

        }

        public void SignUpUser(User user)
        {
            try
            {
                user.Role = Role.Regular;
                user.Password = utilities.HashPassword(user.Password);
                eFCoreRepositories.SignUpUser(user);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public bool LoginUser(User user)
        {
            try
            {
                user.Password = utilities.HashPassword(user.Password);
                bool isLogin = eFCoreRepositories.LoginUser(user);

                if (isLogin)
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
            }
        }

        public bool LoginAdminUser(User user)
        {
            try
            {
                bool isAdmin = eFCoreRepositories.LoginAdminUser(user);

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
            }
        }

        public bool IsEmailExist(string email)
        {
            try
            {
                bool isExist = eFCoreRepositories.IsEmailExist(email);

                if (isExist)
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
            }
        }
    }
}
