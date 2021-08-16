using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Api
{
    [Table("CategoryAttribute")]
    public class CategoryAttribute : IEntity
    {
        [Key]
        [Column(Order = 1)]
        public int CategoryId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int AttributeId { get; set; }
    }
}
