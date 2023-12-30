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
        }

        public void CreateUser(User user)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = ConnectToDB();
                cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                cmd.Parameters.AddWithValue("@LastName", user.LastName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
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
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
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

        public string ValidateAdminLogin(Admin admin)
        {
            SqlConnection? connection = default;
            SqlCommand? cmd;

            try
            {
                (connection, cmd) = ConnectToDB();
                string receivedPassword = string.Empty;
                cmd.Parameters.AddWithValue("@Email", admin.AdminEmail);
                cmd.CommandText = "AdminLoginProcedure";
                var result = cmd.ExecuteScalar();

                if (result!=null && result != DBNull.Value)
                {
                    receivedPassword = result.ToString();
                    connection?.Close();
                }
                return receivedPassword;
                
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
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
        }

        public List<Product> GetProducts()
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            List<Product> productList = new();

            try
            {
                (connection, cmd) = ConnectToDB();
                cmd.CommandText = "GetProductProcedure";
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new()
                    {
                        ProductId = (Guid)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = reader["ProductPrice"].ToString(),
                        ProductDetail = reader["ProductDetail"].ToString(),
                        ProductImage = reader["ProductImage"].ToString()
                    };

                    productList.Add(product);
                }

                return productList;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Product GetProductById(Product product)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = ConnectToDB();
                cmd.CommandText = "GetProductByIdProcedure";
                cmd.Parameters.AddWithValue("@ProductId", product.ProductId);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                     var selectedProduct = new Product  
                    {
                        ProductId = (Guid)reader["ProductId"],
                        ProductName = reader["ProductName"].ToString(),
                        ProductPrice = reader["ProductPrice"].ToString(),
                        ProductDetail = reader["ProductDetail"].ToString(),
                        ProductImage = reader["ProductImage"].ToString()
                    };
                    return selectedProduct;
                }else
                {
                    return null;
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void StoreProduct(Product product)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = ConnectToDB();
                cmd.Parameters.AddWithValue("@CarName", product.ProductName);
                cmd.Parameters.AddWithValue("@CarPrice", product.ProductPrice);
                cmd.Parameters.AddWithValue("@CarDetails", product.ProductDetail);
                cmd.Parameters.AddWithValue("@CarImage", product.ProductImage);

                cmd.CommandText = "StoreCarProductProcedure";
                var x = cmd.ExecuteScalar();
                connection?.Close();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool IsEmailExist(string email)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = ConnectToDB();
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
