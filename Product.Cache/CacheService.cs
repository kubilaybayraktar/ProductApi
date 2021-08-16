using Product.Api;
using System;
using System.Collections.Generic;

namespace Product.Cache
{
    public class CacheService : ICacheService
    {
        private readonly ICacheProvider _cacheProvider;
        private readonly ProductHelper _productHelper;
        private readonly CategoryHelper _categoryHelper;

        public CacheService(ICacheProvider cacheProvider,
                            ProductHelper productHelper,
                            CategoryHelper categoryHelper)
        {
            _cacheProvider = cacheProvider;
            _productHelper = productHelper;
            _categoryHelper = categoryHelper;
        }

        public List<ProductModel> GetAllProducts(short? status)
        {
            List<ProductModel> products = _cacheProvider.GetFromCache<List<ProductModel>>(CacheKeys.Products(status));

            if (products.IsNull())
            {
                products = _productHelper.GetAllProducts(status);
                _cacheProvider.SetCache(CacheKeys.Products(status), products, DateTimeOffset.Now.AddHours(1));
            }

            return products;
        }

        public List<LookupCategoryModel> GetAllCategories(short? status)
        {
            List<LookupCategoryModel> categories = _cacheProvider.GetFromCache<List<LookupCategoryModel>>(CacheKeys.Categories(status));

            if (categories.IsNull())
            {
                categories = _categoryHelper.GetAllCategories(status);
                _cacheProvider.SetCache(CacheKeys.Categories(status), categories, DateTimeOffset.Now.AddHours(1));
            }

            return categories;
        }

        public void ResetCache()
        {
            _cacheProvider.ClearCache(CacheKeys.Products(null));
            _cacheProvider.ClearCache(CacheKeys.Products(0));
            _cacheProvider.ClearCache(CacheKeys.Products(1));
            _cacheProvider.ClearCache(CacheKeys.Categories(null));
            _cacheProvider.ClearCache(CacheKeys.Categories(0));
            _cacheProvider.ClearCache(CacheKeys.Categories(1));
        }



    }
}