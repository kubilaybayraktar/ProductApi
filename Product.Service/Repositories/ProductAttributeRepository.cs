using System.Linq;

namespace Product.Api
{
    public class ProductAttributeRepository : BaseRepository<ProductAttribute>, IProductAttributeRepository
    {
        public ProductAttributeRepository(DataContext context) : base(context)
        {
        }

        public IQueryable<ProductAttribute> GetByAttributeId(int attributeId)
        {
            return DbContext.ProductAttributes.GetByAttributeId(attributeId);
        }
    }
}
