using System.ComponentModel.DataAnnotations;

namespace Product.Api
{
    public class NameQueryModel
    {
        [Required]
        public string Name { get; set; }
    }
}
