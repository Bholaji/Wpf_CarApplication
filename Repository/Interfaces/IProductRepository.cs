using Models;

namespace Repository.Interfaces
{
    public interface IProductRepository
    {
        Product GetProductById(Guid productId);
        List<Product> GetProducts();
        void StoreProduct(Product product);
    }
}