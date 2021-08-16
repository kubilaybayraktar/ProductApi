using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Api
{
    public class LookupAttributeModel: IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short Status { get; set; }
    }
}
