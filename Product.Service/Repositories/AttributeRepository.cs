namespace Product.Api
{
    public class AttributeRepository : BaseItemRepository<LookupAttribute>, IAttributeRepository
    {
        public AttributeRepository(DataContext context) : base(context)
        {
        }
    }
}
