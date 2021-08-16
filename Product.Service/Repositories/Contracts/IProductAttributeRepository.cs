using System.Linq;

namespace Product.Api
{
    public interface IProductAttributeRepository : IRepository<ProductAttribute>
    {
        IQueryable<ProductAttribute> GetByAttributeId(int attributeId);
    }
}
