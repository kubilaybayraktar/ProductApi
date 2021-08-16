using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Cache
{
    public static class CacheKeys
    {
        public static string Products(short? status) => "products_" + (status.HasValue ? status.Value.ToString() : "");
        public static string Categories(short? status) => "categories_" + (status.HasValue ? status.Value.ToString() : "");
    }
}
