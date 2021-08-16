using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Product.Api;
using Product.Cache;
using System;
using System.Configuration;
using System.Reflection;

namespace Product.Performance
{
    public abstract class TestBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly CacheService _cacheService;

        public TestBase()
        {
            string assemblyPath = new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath;
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(assemblyPath);
            string conn = cfg.ConnectionStrings.ConnectionStrings["DbContext"].ConnectionString;

            DbContextOptionsBuilder<DataContext> dbContextOptions = new();
            dbContextOptions.UseSqlServer(conn);

            DataContext context = new(dbContextOptions.Options);
            _unitOfWork = new UnitOfWork(context);

            ProductHelper productHelper = new(_unitOfWork);
            CategoryHelper categoryHelper = new(_unitOfWork);
            IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
            CacheProvider cacheProvider = new(cache);
            _cacheService = new(cacheProvider, productHelper, categoryHelper);
        }
    }
}
