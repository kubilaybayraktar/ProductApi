using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public static class ModelExtensions
    {
        public static List<LookupCategoryModel> JoinAttributes(this List<LookupCategoryModel> categories, IUnitOfWork unitOfWork)
        {
            //Get attributes
            List<CategoryAttributeValueModel> categoryAttributes = unitOfWork.CategoryAttributes.GetAttributes().AsModels();

            //Set attributes
            categories.ForEach(x => x.Attributes = categoryAttributes.Where(y => y.CategoryId == x.Id).Select(x => x.Name).ToList());

            return categories;
        }

        public static LookupCategoryModel JoinAttribute(this LookupCategoryModel category, IUnitOfWork unitOfWork)
        {
            //Get attributes
            category.Attributes = unitOfWork.CategoryAttributes.GetAttributesByCategoryId(category.Id);

            return category;
        }
    }
}
