using System.ComponentModel.DataAnnotations;

namespace Product.Api
{
    public class PriceRangeQueryModel
    {
        [Range(1, int.MaxValue)]
        public int Min { get; set; }
        [Range(1, int.MaxValue)]
        public int Max { get; set; }
    }
}
