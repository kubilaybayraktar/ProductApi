using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Api.Tests
{
    public class ModelCreator
    {
        public static string GetRandomName
        {
            get
            {
                return Guid.NewGuid().ToString().Substring(0, 5);
            }
        }

        public static string GetRandomProductName
        {
            get
            {
                return "Product_" + GetRandomName;
            }
        }

        public static string GetRandomCategoryName
        {
            get
            {
                return "Cat_" + GetRandomName;
            }
        }

        public static string GetRandomAttributeName
        {
            get
            {
                return "Attr_" + GetRandomName;
            }
        }

        public static ProductBodyModel GetProductBodyModel(int categoryId, List<int> attributeIds)
        {
            List<ProductAttributeBodyModel> attributes = new();
            foreach (int attributeId in attributeIds)
                attributes.Add(new() { Id = attributeId, Value = GetRandomName });

            return new()
            {
                CategoryId = categoryId,
                Name = GetRandomProductName,
                Price = 10,
                Attributes = attributes
            };
        }

        public static UpdateProductBodyModel GetUpdateProductBodyModel(List<int> attributeIds) =>
            GetUpdateProductBodyModel(attributeIds, GetRandomProductName, 10);

        public static UpdateProductBodyModel GetUpdateProductBodyModel(List<int> attributeIds, int price) =>
            GetUpdateProductBodyModel(attributeIds, GetRandomProductName, price);

        public static UpdateProductBodyModel GetUpdateProductBodyModel(List<int> attributeIds, string name, int price)
        {
            List<ProductAttributeBodyModel> attributes = new();
            foreach (int attributeId in attributeIds)
                attributes.Add(new() { Id = attributeId, Value = GetRandomName });

            return new()
            {
                Name = name,
                Price = price,
                Attributes = attributes
            };
        }

        public static AddCategoryBodyModel GetAddCategoryBodyModel(List<int> attributes)
        {
            return new()
            {
                Name = GetRandomCategoryName,
                Attributes = attributes
            };
        }

        public static UpdateCategoryBodyModel GetUpdateCategoryBodyModel()
        {
            return new()
            {
                Name = GetRandomCategoryName
            };
        }
    }
}
