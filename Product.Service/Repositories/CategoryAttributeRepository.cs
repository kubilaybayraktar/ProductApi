using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public class CategoryAttributeRepository : BaseRepository<CategoryAttribute>, ICategoryAttributeRepository
    {
        public CategoryAttributeRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<CategoryAttribute> GetByCategoryId(int categoryId)
        {
            return DbContext.CategoryAttributes.GetByCategoryId(categoryId);
        }

        public IQueryable<CategoryAttribute> GetByCategoryIds(List<int> categoryIds)
        {
            return DbContext.CategoryAttributes.GetByCategoryIds(categoryIds);
        }

        public List<CategoryAttributeValue> GetAttributes()
        {
            return DbContext.CategoryAttributeValues.FromSqlRaw($"select ca.CategoryId, la.[Name] from CategoryAttribute ca inner join LookupAttribute la on ca.AttributeId=la.Id").ToList();
        }

        public List<string> GetAttributesByCategoryId(int categoryId)
        {
            return DbContext.StringValues.FromSqlRaw($"select la.[Name] as [Value] from CategoryAttribute ca inner join LookupAttribute la on ca.AttributeId=la.Id where CategoryId={categoryId}").Select(x => x.Value).ToList();
        }

        public IQueryable<CategoryAttribute> GetByAttributeId(int attributeId)
        {
            return DbContext.CategoryAttributes.GetByAttributeId(attributeId);
        }
    }
}
