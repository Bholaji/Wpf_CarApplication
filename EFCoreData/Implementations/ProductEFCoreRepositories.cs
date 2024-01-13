using EFCoreData.Interface;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreData.Implementations
{
    public class ProductEFCoreRepositories : IProductEFCoreRepositories
    {
        private readonly RepositoryDB _dbContext;

        public ProductEFCoreRepositories(RepositoryDB dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();
        }

        public List<Product> LoadProducts()
        {
            List<Product> allProducts = _dbContext.Product.ToList();
            return allProducts;
        }

        public Product LoadProductById(Guid productId)
        {
            var selectedProduct = _dbContext.Product?.FirstOrDefault(x => x.ProductId == productId);
            return selectedProduct;
        }

        public void StoreProduct(Product product)
        {
            _dbContext.Product?.Add(product);
            _dbContext.SaveChanges();
        }

    }
}
