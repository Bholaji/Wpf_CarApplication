using EFCoreData.Implementations;
using EFCoreData.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementaions
{
    public class ProductServiceEFCore : IProductEFCoreRepositories
    {
        private readonly RepositoryDB _dbContext;
        private readonly IProductEFCoreRepositories productEFCore;
        public ProductServiceEFCore()
        {
            _dbContext = new RepositoryDB();
            productEFCore = new ProductEFCoreRepositories(_dbContext);
        }

        public List<Product> LoadProducts()
        {
            try
            {
                List<Product> allProduct = productEFCore.LoadProducts();
                return allProduct;
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
                var selectedProduct = productEFCore.LoadProductById(productId);
                return selectedProduct;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void StoreProduct(Product product)
        {
            try
            {
                productEFCore.StoreProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
