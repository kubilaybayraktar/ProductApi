using System.Text.Json.Serialization;

namespace Product.Api
{
    public class CategoryAttributeValueModel
    {
        [JsonIgnore]
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
