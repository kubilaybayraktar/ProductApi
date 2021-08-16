using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public static class CategoryAttributeQueries
    {
        public static IQueryable<CategoryAttribute> GetByCategoryId(this DbSet<CategoryAttribute> categoryAttributes, int categoryId)
        {
            return categoryAttributes.Where(x => x.CategoryId == categoryId);
        }

        public static IQueryable<CategoryAttribute> GetByCategoryIds(this DbSet<CategoryAttribute> categoryAttributes, List<int> categoryIds)
        {
            return categoryAttributes.Where(x => categoryIds.Contains(x.CategoryId));
        }

        public static IQueryable<CategoryAttribute> GetByAttributeId(this DbSet<CategoryAttribute> categoryAttributes, int attributeId)
        {
            return categoryAttributes.Where(x => x.AttributeId == attributeId);
        }
    }
}
