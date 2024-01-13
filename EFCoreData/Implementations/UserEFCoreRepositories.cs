using EFCoreData.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreData.Implementations
{
    public class UserEFCoreRepositories : IUserEFCoreRepositories
    {
        private readonly RepositoryDB _dbContext;

        public UserEFCoreRepositories(RepositoryDB dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public void SignUpUser(User user)
        {
            try
            {
                _dbContext.User?.Add(user);
                _dbContext.SaveChanges();
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
                var loginUser = _dbContext.User?.SingleOrDefault(u => u.Email == user.Email && u.Role == Role.Regular);

                if (loginUser != null)
                {
                    if (loginUser.Password == user.Password)
                    {
                        return true;
                    }
                }

                return false;
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
                var loginUser = _dbContext.User?.SingleOrDefault(u => u.Email == user.Email && u.Role == Role.Admin);

                if (loginUser != null)
                {
                    if (loginUser.Password == user.Password)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsEmailExist(string email)
        {
            var isEmailExist = _dbContext?.User?.SingleOrDefault(u => u.Email == email);

            if( isEmailExist != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
