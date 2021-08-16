using System.Collections.Generic;

namespace Product.Api
{
    public static class Converter
    {
        public static List<ProductAttribute> ToProductAttributes(this List<ProductAttributeBodyModel> list, int productId)
        {
            List<ProductAttribute> result = new();

            foreach (ProductAttributeBodyModel item in list)
            {
                result.Add(new()
                {
                    AttributeId = item.Id,
                    AttributeValue = item.Value,
                    ProductId = productId
                });
            }

            return result;
        }
    }
}
