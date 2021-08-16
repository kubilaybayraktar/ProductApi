using BenchmarkDotNet.Attributes;
using System.Linq;

namespace Product.Performance
{
    [HtmlExporter]
    public class BenchmarkHarness : TestBase
    {
        [Benchmark]
        public void GetProducts()
        {
            _unitOfWork.Products.GetList().Where(x => x.Status == 1).ToList();
        }

        [Benchmark]
        public void GetProductsByCache()
        {
            _cacheService.GetAllProducts(1);
        }
    }
}
