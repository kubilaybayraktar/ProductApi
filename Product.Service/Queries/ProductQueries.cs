using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Product.Api
{
    public static class ProductQueries
    {
        public static IQueryable<Product> GetByCategoryId(this DbSet<Product> products, int categoryId)
        {
            return products.Where(x => x.CategoryId == categoryId);
        }
    }
}
