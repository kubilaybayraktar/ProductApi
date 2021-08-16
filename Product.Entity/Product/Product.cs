using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Api
{
    [Table("Product")]
    public class Product : IItem
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Status { get; set; }

        [ForeignKey("CategoryId")]
        public virtual LookupCategory Category { get; set; }

        public virtual List<ProductAttribute> ProductAttributes { get; set; }
    }
}
