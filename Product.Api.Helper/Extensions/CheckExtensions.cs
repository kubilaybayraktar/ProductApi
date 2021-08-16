namespace Product.Api
{
    public static class CheckExtensions
    {
        public static T CheckExists<T>(this T entity) where T : IModel
        {
            if (entity.IsNull())
                throw new UserFriendlyError($"{typeof(T).Name} does not exist!", statusCode: 404);

            return entity;
        }
    }
}
