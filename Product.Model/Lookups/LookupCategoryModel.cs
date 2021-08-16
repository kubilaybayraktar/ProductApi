using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Product.Api
{
    public class LookupCategoryModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
        public List<string> Attributes { get; set; }
    }
}
