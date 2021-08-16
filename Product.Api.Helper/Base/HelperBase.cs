using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public abstract class HelperBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        public HelperBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected void ValidateCategory(int categoryId)
        {
            categoryId.IsValid();
            _unitOfWork.Categories.GetById(categoryId).Validate();
        }

        protected void ValidateAttributes(int categoryId, List<int> attributeIds)
        {
            categoryId.IsValid();
            attributeIds.IsValid();

            List<int> existingAttributeIds = _unitOfWork.CategoryAttributes.GetByCategoryId(categoryId).Select(x => x.AttributeId).ToList();

            if (existingAttributeIds.Count < attributeIds.Count) //not all attributes need to be supplied
            {
                throw new UserFriendlyError("There were invalid values for supplied attributeIds", 422);
            }
        }

        public static short ActiveStatusId
        {
            get
            {
                return (short)Statuses.Active;
            }
        }

        public static short DeletedStatusId
        {
            get
            {
                return (short)Statuses.Deleted;
            }
        }
    }
}
