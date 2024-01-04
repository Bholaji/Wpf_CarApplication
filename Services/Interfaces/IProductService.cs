using Models;

namespace Services.Interfaces
{
    public interface IProductService
    {
        List<Product> LoadProduct();
        Product LoadProductById(Guid productId);
        void StoreProductToDB(Product product);
    }
}