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
        private readonly SQLConnection sqlConnection;
        public UserRepository()
        {
            sqlConnection = new SQLConnection();
        }

        public void CreateUser(User user)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.CommandText = "SignUpProcedure";
                var x = cmd.ExecuteScalar();
                connection?.Close();
            }
            catch (Exception ex)
            {
                connection?.Close();
                throw;
            }
            finally
            {
                connection?.Close();
            }
        }

        public bool ValidateLogin(User user)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            bool loginSuccessful = false;

            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.CommandText = "LogInProcedure";
                var result = cmd.ExecuteScalar();

                if (result != null && result.ToString() == "SUCCESS")
                {
                    loginSuccessful = true;
                }

                connection?.Close();

            }
            catch (Exception ex)
            {
                connection?.Close();
                throw;
            }
            finally
            {
                connection?.Close();
            }

            return loginSuccessful;
        }

        public bool ValidateAdminLogin(User user)
        {
            SqlConnection? connection = default;
            SqlCommand? cmd;
            bool loginSuccessful = false;

            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
                string receivedPassword = string.Empty;
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Role", user.Role);
                cmd.CommandText = "AdminLoginProcedure";
                var result = cmd.ExecuteScalar();

                if (result != null && result.ToString() == "SUCCESS")
                {
                    loginSuccessful = true;
                }

                connection?.Close();


            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
            return loginSuccessful;
        }

        public string GetStoredHashedPassword(User user)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;

            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
                string hashedPassword = string.Empty;
                cmd.Parameters.AddWithValue("@Email", user.Email);
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
            finally
            {
                connection?.Close();
            }
        }

        public bool IsEmailExist(string email)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
                cmd.Parameters.AddWithValue("@Email", email);
                /*cmd.Parameters.Add("@Exist", SqlDbType.Bit).Direction = ParameterDirection.Output;*/
                SqlParameter existsParam = new ("@Exists", SqlDbType.Bit);
                existsParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(existsParam);

                cmd.CommandText = "EmailExistProcedure";
                cmd.ExecuteNonQuery();

                bool emailExist = Convert.ToBoolean(cmd.Parameters["@Exists"].Value);

                return emailExist;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

    }
}
