using System.Linq;

namespace Product.Api
{
    public class ProductRepository : BaseItemRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<Product> GetByCategoryId(int categoryId)
        {
            return DbContext.Products.GetByCategoryId(categoryId);
        }

        public IQueryable<Product> GetByPriceRange(int min, int max)
        {
            return DbContext.Products.Where(x => x.Price >= min && x.Price <= max);
        }
    }
}
