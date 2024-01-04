using Models;
using Repository.Implementations;
using Repository.Interfaces;
using Services.Interfaces;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using serviceUtility = Services.Utilities.Utilities;

namespace Services.Implementaions
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IUtilities utilities;
        public ProductService()
        {
            utilities = new serviceUtility();
            productRepository = new ProductRepository();
        }

        public List<Product> LoadProduct()
        {
            try
            {
                return productRepository.GetProducts(); ;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Product LoadProductById(Guid productId)
        {
            try
            {
                return productRepository.GetProductById(productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void StoreProductToDB(Product product)
        {
            try
            {
                productRepository.StoreProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
