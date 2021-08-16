using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public interface ICategoryAttributeRepository : IRepository<CategoryAttribute>
    {
        IQueryable<CategoryAttribute> GetByCategoryId(int categoryId);
        IQueryable<CategoryAttribute> GetByCategoryIds(List<int> categoryIds);
        List<CategoryAttributeValue> GetAttributes();
        List<string> GetAttributesByCategoryId(int categoryId);
    }
}
