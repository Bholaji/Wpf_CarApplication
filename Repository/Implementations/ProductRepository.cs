using Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly SQLConnection sqlConnection;
        public ProductRepository()
        {
            sqlConnection = new SQLConnection();
        }

        public List<Product> GetProducts()
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            List<Product> productList = new();

            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
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
            finally
            {
                connection?.Close();
            }
        }

        public Product GetProductById(Guid productId)
        {
            SqlConnection? connection = default;
            SqlCommand cmd;
            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
                cmd.CommandText = "GetProductByIdProcedure";
                cmd.Parameters.AddWithValue("@ProductId", productId);
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
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection?.Close();
            }
        }

        public void StoreProduct(Product product)
        {
            SqlCommand cmd;
            SqlConnection? connection = default;

            try
            {
                (connection, cmd) = sqlConnection.ConnectToDB();
                cmd.Parameters.AddWithValue("@CarName", product.ProductName);
                cmd.Parameters.AddWithValue("@CarPrice", product.ProductPrice);
                cmd.Parameters.AddWithValue("@CarDetails", product.ProductDetail);
                cmd.Parameters.AddWithValue("@CarImage", product.ProductImage);

                cmd.CommandText = "StoreCarProductProcedure";
                var x = cmd.ExecuteScalar();
            }
            catch (Exception ex)
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
