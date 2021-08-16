using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Api
{
    [Table("LookupAttribute")]
    public class LookupAttribute : IItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
    }
}
