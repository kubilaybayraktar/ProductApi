using System.Collections.Generic;

namespace Product.Api
{
    public class ProductAttributeComparer : IEqualityComparer<ProductAttribute>
    {
        public int GetHashCode(ProductAttribute obj)
        {
            if (obj == null)
                return 0;

            return obj.AttributeId.GetHashCode();
        }

        public bool Equals(ProductAttribute x, ProductAttribute y)
        {
            if (object.ReferenceEquals(x, y))
                return true;

            if (x is null || y is null)
                return false;

            return x.AttributeId == y.AttributeId && x.ProductId == y.ProductId;
        }
    }
}