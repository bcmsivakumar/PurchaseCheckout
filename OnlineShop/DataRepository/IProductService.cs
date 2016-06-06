using OnlineShop;

namespace OnlineShop.DataRepository
{
    public interface IProductService
    {
        Product GetProductByProductCode(string productCode);
    }
}