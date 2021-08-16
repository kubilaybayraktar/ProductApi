using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Api
{
    public class CategoryAttributeModel: IModel
    {
        public int CategoryId { get; set; }
        public int AttributeId { get; set; }
    }
}
