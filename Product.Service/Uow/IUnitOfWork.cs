namespace Product.Api
{
    public interface IUnitOfWork
    {
        ProductRepository Products { get; }
        ProductAttributeRepository ProductAttributes { get; }
        CategoryRepository Categories { get; }
        AttributeRepository Attributes { get; }
        CategoryAttributeRepository CategoryAttributes { get; }
        void Commit();
    }
}
