using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Product.Api
{
    public class ProductModel : IModel
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Status { get; set; }
        public LookupCategoryModel Category { get; set; }

        public List<ProductAttributeModel> ProductAttributes { get; set; }

    }
}
