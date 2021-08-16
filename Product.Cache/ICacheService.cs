using Product.Api;
using System.Collections.Generic;

namespace Product.Cache
{
    public interface ICacheService
    {
        List<ProductModel> GetAllProducts(short? status);
        List<LookupCategoryModel> GetAllCategories(short? status);
        void ResetCache();
    }
}
