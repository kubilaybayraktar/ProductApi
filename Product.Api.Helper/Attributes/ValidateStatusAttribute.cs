using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public class ValidateStatusAttribute : Attribute, IModelValidator
    {
        public string ErrorMessage { get; set; }

        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var model = context.Model as short?;
            bool exists = model.HasValue && Enum.IsDefined(typeof(Statuses), model.Value);

            if (!exists)
            {
                ErrorMessage = "Invalid status parameter!";
                return new List<ModelValidationResult> { new ModelValidationResult("", ErrorMessage) };
            }

            return Enumerable.Empty<ModelValidationResult>();
        }
    }
}
