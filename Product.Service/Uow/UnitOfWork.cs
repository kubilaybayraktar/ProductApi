using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Api
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _dbContext;
        private ProductRepository _products;
        private ProductAttributeRepository _productCategoryAttributes;
        private CategoryRepository _categories;
        private AttributeRepository _attributes;
        private CategoryAttributeRepository _categoryAttributes;
        public UnitOfWork(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductRepository Products
        {
            get
            {
                return _products ??
                    (_products = new ProductRepository(_dbContext));
            }
        }

        public ProductAttributeRepository ProductAttributes
        {
            get
            {
                return _productCategoryAttributes ??
                    (_productCategoryAttributes = new ProductAttributeRepository(_dbContext));
            }
        }

        public CategoryRepository Categories
        {
            get
            {
                return _categories ??
                    (_categories = new CategoryRepository(_dbContext));
            }
        }

        public AttributeRepository Attributes
        {
            get
            {
                return _attributes ??
                    (_attributes = new AttributeRepository(_dbContext));
            }
        }

        public CategoryAttributeRepository CategoryAttributes
        {
            get
            {
                return _categoryAttributes ??
                    (_categoryAttributes = new CategoryAttributeRepository(_dbContext));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
