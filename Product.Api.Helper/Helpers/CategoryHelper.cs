using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public class CategoryHelper : HelperBase
    {
        #region Ctor
        public CategoryHelper(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        #endregion

        #region Get
        public List<LookupCategoryModel> GetAllCategories(short? status = null)
        {
            //Get all categories
            IQueryable<LookupCategory> categories = _unitOfWork.Categories.GetList();
            if (status.HasValue) categories = categories.Where(x => x.Status == status.Value);

            //Return as models
            return categories.ToList().AsModels().JoinAttributes(_unitOfWork);
        }

        public LookupCategoryModel GetCategoryByName(string name)
        {
            //Get category
            LookupCategoryModel category = _unitOfWork.Categories.GetByField(Settings.CategoryNameField, name).Validate().AsModel().JoinAttribute(_unitOfWork);

            return category;
        }

        #endregion

        #region Add
        public LookupCategoryModel AddCategory(AddCategoryBodyModel model)
        {
            //Validation
            ValidateAttributes(model.Attributes);

            //Add category
            LookupCategory category = new()
            {
                Name = model.Name,
                Status = ActiveStatusId
            };
            _unitOfWork.Categories.Insert(category);
            _unitOfWork.Commit();

            //Add attributes
            foreach (int attributeId in model.Attributes)
                _unitOfWork.CategoryAttributes.Insert(new()
                {
                    AttributeId = attributeId,
                    CategoryId = category.Id
                });

            //Save changes
            _unitOfWork.Commit();

            return category.AsModel();
        }

        private void ValidateAttributes(List<int> ids)
        {
            int count = _unitOfWork.Attributes.GetListByIds(ids).Count;

            if (count < ids.Count)
                throw new UserFriendlyError("Please check your attribute values!", 422);
        }
        #endregion

        #region Update
        public void UpdateCategory(int categoryId, string name)
        {
            //Get & Validate
            LookupCategory category = _unitOfWork.Categories.GetById(categoryId).Validate();

            //Update
            category.Name = name;
            _unitOfWork.Categories.Update(category);

            //Save changes
            _unitOfWork.Commit();
        }
        #endregion

        #region Category Attributes
        public void AddAttribute(int categoryId, int attributeId)
        {
            //Validation category
            bool exists = _unitOfWork.CategoryAttributes.GetByCategoryId(categoryId).Any(x => x.AttributeId == attributeId);
            if (exists) throw new UserFriendlyError($"This attribute has already been added to this category {attributeId}", 400);

            //Validate attribute
            _unitOfWork.Attributes.GetById(attributeId).Validate();

            //Add
            CategoryAttribute categoryAttribute = new()
            {
                AttributeId = attributeId,
                CategoryId = categoryId
            };
            _unitOfWork.CategoryAttributes.Insert(categoryAttribute);

            //Save changes
            _unitOfWork.Commit();
        }

        public void RemoveAttribute(int categoryId, int attributeId)
        {
            //Validate
            CategoryAttribute categoryAttribute = _unitOfWork.CategoryAttributes.GetByCategoryId(categoryId).SingleOrDefault(x => x.AttributeId == attributeId);
            if (categoryAttribute.IsNull())
                throw new UserFriendlyError($"This attribute has already been removed/not found for this category {attributeId}", 400);

            //Delete
            _unitOfWork.CategoryAttributes.Delete(categoryAttribute);

            //Save changes
            _unitOfWork.Commit();
        }
        #endregion

        #region Delete
        public void DeleteCategory(int id)
        {
            //Get & Validate
            LookupCategory category = _unitOfWork.Categories.GetById(id).Validate();

            //Update
            category.Status = DeletedStatusId;
            _unitOfWork.Categories.Update(category);

            //Save changes
            _unitOfWork.Commit();
        }
        #endregion

        #region Attributes
        public List<LookupAttributeModel> GetAllAttributes(Statuses? status = null)
        {
            //Get all attributes
            IQueryable<LookupAttribute> attributes = _unitOfWork.Attributes.GetList();
            if (status.HasValue) attributes = attributes.Where(x => x.Status == (short)status);

            return attributes.ToList().AsModels();
        }

        public LookupAttributeModel AddAttribute(string name)
        {
            //Add attribute
            LookupAttribute attribute = new()
            {
                Name = name,
                Status = ActiveStatusId
            };
            _unitOfWork.Attributes.Insert(attribute);

            //Save changes
            _unitOfWork.Commit();

            return attribute.AsModel();
        }

        public void UpdateAttribute(int id, string name)
        {
            //Get & Validate
            LookupAttribute attribute = _unitOfWork.Attributes.GetById(id).Validate();

            //Update
            attribute.Name = name;
            _unitOfWork.Attributes.Update(attribute);

            //Save changes
            _unitOfWork.Commit();
        }

        public void DeleteAttribute(int id)
        {
            //Get & Validate
            LookupAttribute attribute = _unitOfWork.Attributes.GetById(id).Validate();

            //Update attribute as deleted
            attribute.Status = DeletedStatusId;
            _unitOfWork.Attributes.Update(attribute);

            //Remove relations with category attributes
            List<CategoryAttribute> categoryAttributes = _unitOfWork.CategoryAttributes.GetByAttributeId(id).ToList();
            _unitOfWork.CategoryAttributes.DeleteList(categoryAttributes);

            //Remove relations with products
            List<ProductAttribute> productAttributes = _unitOfWork.ProductAttributes.GetByAttributeId(id).ToList();
            _unitOfWork.ProductAttributes.DeleteList(productAttributes);

            //Save changes
            _unitOfWork.Commit();
        }
        #endregion
    }
}
