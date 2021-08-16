using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Product.Api
{
    public class ProductAttributeModel: IModel
    {
        [JsonIgnore]
        public int ProductId { get; set; }
        [JsonIgnore]
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
        [JsonIgnore]
        public LookupAttributeModel Attribute { get; set; }
        public string AttributeName
        {
            get
            {
                return Attribute?.Name;
            }
        }
    }
}
