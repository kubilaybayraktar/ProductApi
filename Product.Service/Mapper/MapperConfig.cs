using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Api
{
    public class MapperConfig
    {
        private static MapperConfiguration _mapperConfig = null;
        public static MapperConfiguration Config
        {
            get
            {
                if (_mapperConfig == null)
                    Initialize();

                return _mapperConfig;
            }
        }


        static void Initialize()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductModel>()
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Category))
                .ForMember(x => x.ProductAttributes, opt => opt.MapFrom(y => y.ProductAttributes));
                cfg.CreateMap<CategoryAttribute, CategoryAttributeModel>();
                cfg.CreateMap<LookupAttribute, LookupAttributeModel>();
                cfg.CreateMap<LookupCategory, LookupCategoryModel>();
                cfg.CreateMap<ProductAttribute, ProductAttributeModel>();
                cfg.CreateMap<CategoryAttributeValue, CategoryAttributeValueModel>();
            });
        }
    }
}
