using System.Collections.Generic;

namespace Product.Api
{
    public static class ModelConverter
    {
        public static List<ProductModel> AsModels(this List<Product> model)
        {
            return MapperConfig.Config.CreateMapper().Map<List<ProductModel>>(model);
        }

        public static ProductModel AsModel(this Product model)
        {
            return MapperConfig.Config.CreateMapper().Map<ProductModel>(model);
        }

        public static CategoryAttributeModel AsModel(this CategoryAttribute model)
        {
            return MapperConfig.Config.CreateMapper().Map<CategoryAttributeModel>(model);
        }

        public static List<CategoryAttributeModel> AsModels(this List<CategoryAttribute> model)
        {
            return MapperConfig.Config.CreateMapper().Map<List<CategoryAttributeModel>>(model);
        }

        public static List<LookupAttributeModel> AsModels(this List<LookupAttribute> model)
        {
            return MapperConfig.Config.CreateMapper().Map<List<LookupAttributeModel>>(model);
        }

        public static LookupAttributeModel AsModel(this LookupAttribute model)
        {
            return MapperConfig.Config.CreateMapper().Map<LookupAttributeModel>(model);
        }

        public static LookupCategoryModel AsModel(this LookupCategory model)
        {
            return MapperConfig.Config.CreateMapper().Map<LookupCategoryModel>(model);
        }

        public static List<LookupCategoryModel> AsModels(this List<LookupCategory> model)
        {
            return MapperConfig.Config.CreateMapper().Map<List<LookupCategoryModel>>(model);
        }

        public static List<ProductAttributeModel> AsModels(this List<ProductAttributeModel> model)
        {
            return MapperConfig.Config.CreateMapper().Map<List<ProductAttributeModel>>(model);
        }

        public static ProductAttributeModel AsModel(this ProductAttributeModel model)
        {
            return MapperConfig.Config.CreateMapper().Map<ProductAttributeModel>(model);
        }

        public static List<CategoryAttributeValueModel> AsModels(this List<CategoryAttributeValue> model)
        {
            return MapperConfig.Config.CreateMapper().Map<List<CategoryAttributeValueModel>>(model);
        }
    }
}
