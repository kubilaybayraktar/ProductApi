using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Api
{
    [Table("ProductAttribute")]
    public class ProductAttribute : IEntity
    {
        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        
        [Key]
        [Column(Order = 2)]
        public int AttributeId { get; set; }
        
        public string AttributeValue { get; set; }
        
        [ForeignKey("AttributeId")]
        public virtual LookupAttribute Attribute { get; set; }
    }
}
