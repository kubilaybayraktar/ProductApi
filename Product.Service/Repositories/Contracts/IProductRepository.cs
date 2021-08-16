using System.Linq;

namespace Product.Api
{
    public interface IProductRepository : IItemRepository<Product>
    {
        IQueryable<Product> GetByCategoryId(int categoryId);
    }
}
