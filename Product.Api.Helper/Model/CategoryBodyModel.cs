using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Product.Api
{
    public class AddCategoryBodyModel: UpdateCategoryBodyModel
    {
        public List<int> Attributes { get; set; } = new();
    }

    public class UpdateCategoryBodyModel
    {
        [Required]
        public string Name { get; set; }
    }
}
