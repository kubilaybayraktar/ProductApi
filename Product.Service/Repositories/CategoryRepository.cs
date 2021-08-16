namespace Product.Api
{
    public class CategoryRepository : BaseItemRepository<LookupCategory>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }
    }
}
