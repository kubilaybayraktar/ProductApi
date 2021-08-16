namespace Product.Api
{
    public static class ValidateExtensions
    {
        public static T Validate<T>(this T entity) where T : IEntity
        {
            if (entity.IsNull())
                throw new UserFriendlyError($"{typeof(T).Name} type entity not found!", statusCode: 400);

            return entity;
        }
    }
}
