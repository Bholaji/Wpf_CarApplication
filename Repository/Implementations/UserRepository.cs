using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq.Expressions;
using System.Windows;
using Xceed.Wpf.Toolkit;
using Repository.Interfaces;

namespace Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
            var user = new User();
        }

        public void CreateUser(User user)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = ConnectToDB();
                cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = user.FirstName;
                cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = user.LastName;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100).Value = user.Password;
                cmd.CommandText = "SignUpProcedure";
                var x = cmd.ExecuteScalar();
                connection?.Close();
            }
            catch (Exception ex)
            {
                connection?.Close();
                throw;
            }
        }

        public void ValidateLogin(User user)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;

            try
            {
                (connection, cmd) = ConnectToDB();
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 50).Value = user.Password;
                cmd.CommandText = "LogInProcedure";
                var result = cmd.ExecuteScalar();

                connection?.Close();
            }
            catch (Exception ex)
            {
                connection?.Close();
                throw;
            }
        }

        public string GetStoredHashedPassword(User user)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;

            try
            {
                (connection, cmd) = ConnectToDB();
                string hashedPassword = string.Empty;
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                cmd.CommandText = "GetHashProcedure";
                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    hashedPassword = result.ToString();
                    connection?.Close();
                }
                return hashedPassword;
            }catch(Exception ex)
            {
                throw new Exception($"Error retrieving hashed password: {ex.Message}");
            }
        }


        public (SqlConnection, SqlCommand) ConnectToDB()
        {
            string connectionString = "Data Source=.;Initial Catalog=CarDB;Integrated Security=True;Connect Timeout=60;Encrypt=False";

            SqlConnection con = new(connectionString);

            con.Open();

            using SqlCommand cmd = new(null, con);

            cmd.CommandType = CommandType.StoredProcedure;

            return (con, cmd);
        }
    }
}
