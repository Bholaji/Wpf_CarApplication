using Models;

namespace EFCoreData.Interface
{
    public interface IProductEFCoreRepositories
    {
        Product LoadProductById(Guid productId);
        List<Product> LoadProducts();
        void StoreProduct(Product product);
    }
}