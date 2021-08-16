using System.Collections.Generic;
using System.Linq;

namespace Product.Api
{
    public class ProductHelper : HelperBase
    {
        #region Ctor
        public ProductHelper(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
        #endregion

        #region Get

        public List<ProductModel> GetAllProducts(short? status = null)
        {
            IQueryable<Product> products = _unitOfWork.Products.GetList();
            if (status.HasValue) products = products.Where(x => x.Status == status.Value);

            return products.ToList().AsModels();
        }

        public ProductModel GetProductById(int id)
        {
            return _unitOfWork.Products.GetById(id).AsModel().CheckExists();
        }

        public ProductModel GetProductByName(string name)
        {
            return _unitOfWork.Products.GetByField(Settings.ProductNameField, name).AsModel().CheckExists();
        }

        public List<ProductModel> GetProductsByCategoryName(string name)
        {
            LookupCategoryModel category = _unitOfWork.Categories.GetByField(Settings.CategoryNameField, name).AsModel().CheckExists();

            return _unitOfWork.Products.GetByCategoryId(category.Id).ToList().AsModels();
        }

        public List<ProductModel> GetProductsByPriceRange(int min, int max)
        {
            return _unitOfWork.Products.GetByPriceRange(min, max).ToList().AsModels();
        }
        #endregion

        #region Add
        public ProductModel AddProduct(ProductBodyModel model)
        {
            //Validate
            ValidateAddProduct(model.CategoryId, model.Attributes);

            //Add Product
            Product product = new Product
            {
                CategoryId = model.CategoryId,
                Name = model.Name,
                Price = model.Price,
                Status = ActiveStatusId
            };

            //Save product & get product id
            _unitOfWork.Products.Insert(product);
            _unitOfWork.Commit();

            //Add product attributes
            if (product.Id > 0 && model.Attributes.IsNotNullOrEmpty())
            {
                List<ProductAttribute> productAttributes = new();
                foreach (ProductAttributeBodyModel newAttribute in model.Attributes)
                {
                    productAttributes.Add(new ProductAttribute
                    {
                        AttributeId = newAttribute.Id,
                        ProductId = product.Id,
                        AttributeValue = newAttribute.Value
                    });
                }

                //save changes
                _unitOfWork.ProductAttributes.InsertList(productAttributes);
                _unitOfWork.Commit();

                product.ProductAttributes = productAttributes;
            }

            return product.AsModel();
        }

        private void ValidateAddProduct(int categoryId, List<ProductAttributeBodyModel> attributes)
        {
            //Validate Category
            ValidateCategory(categoryId);

            //Validate Attributes
            if (attributes.IsNotNullOrEmpty())
                ValidateAttributes(categoryId, attributes.Select(x => x.Id).ToList());

        }
        #endregion

        #region Update
        public void UpdateProduct(int id, UpdateProductBodyModel model)
        {
            //Get Product & Validate
            Product product = _unitOfWork.Products.GetById(id).Validate();

            //Validate
            ValidateAddProduct(product.CategoryId, model.Attributes);

            //Update Product
            product.Name = model.Name;
            product.Price = model.Price;
            _unitOfWork.Products.Update(product);

            //Update Attributes
            UpdateProductAttributes(id, model);

            //Save Changes
            _unitOfWork.Commit();
        }

        private void UpdateProductAttributes(int id, UpdateProductBodyModel model)
        {
            if (model.Attributes.IsNullOrEmpty()) return;

            //Find attributes to add or remove
            List<ProductAttribute> prevProductAttributes = _unitOfWork.ProductAttributes.GetListByField(Settings.ProductAttributeProductId, id).ToList();
            List<ProductAttribute> newProductAttributes = model.Attributes.ToProductAttributes(id);

            //Compare attributes
            List<ProductAttribute> addProductAttributes = newProductAttributes.Except(prevProductAttributes, new ProductAttributeComparer()).ToList();
            List<ProductAttribute> removeProductAttributes = prevProductAttributes.Except(newProductAttributes, new ProductAttributeComparer()).ToList();
            List<ProductAttribute> updateItems = new();

            foreach (ProductAttribute prevItem in prevProductAttributes)
            {
                ProductAttribute updateItem = newProductAttributes.SingleOrDefault(x => x.ProductId == prevItem.ProductId && x.AttributeId == prevItem.AttributeId);
                if (updateItem.IsNull()) continue;

                updateItems.Add(updateItem);
            }

            //Update / Delete / Insert
            _unitOfWork.ProductAttributes.UpdateList(updateItems);
            _unitOfWork.ProductAttributes.DeleteList(removeProductAttributes);
            _unitOfWork.ProductAttributes.InsertList(addProductAttributes);
        }
        #endregion

        #region Delete
        public void DeleteProduct(int id)
        {
            //Soft delete product
            Product product = _unitOfWork.Products.GetById(id);
            product.Status = DeletedStatusId;
            _unitOfWork.Products.Update(product);

            //Save changes
            _unitOfWork.Commit();
        }
        #endregion
    }
}
