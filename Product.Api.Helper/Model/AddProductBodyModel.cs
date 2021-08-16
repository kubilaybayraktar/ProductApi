using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Api
{
    public class ProductBodyModel : UpdateProductBodyModel
    {
        [Required]
        public int CategoryId { get; set; }
    }

    public class UpdateProductBodyModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public decimal Price { get; set; }
        public List<ProductAttributeBodyModel> Attributes { get; set; } = new();
    }

    public class ProductAttributeBodyModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}
