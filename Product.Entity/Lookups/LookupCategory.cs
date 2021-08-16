using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Api
{
    [Table("LookupCategory")]
    public class LookupCategory : IItem
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
    }
}
