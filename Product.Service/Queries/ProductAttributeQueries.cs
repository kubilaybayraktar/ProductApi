using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Product.Api
{
    public static class ProductAttributeQueries
    {
        public static IQueryable<ProductAttribute> GetByCategoryId(this DbSet<ProductAttribute> productAttributes, int productId)
        {
            return productAttributes.Where(x => x.ProductId == productId);
        }

        public static IQueryable<ProductAttribute> GetByAttributeId(this DbSet<ProductAttribute> productAttributes, int attributeId)
        {
            return productAttributes.Where(x => x.AttributeId == attributeId);
        }
    }
}
