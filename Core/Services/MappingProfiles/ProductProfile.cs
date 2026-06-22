using AutoMapper;
using Domain.Entities.ProductModel;
using Shared.Dtos;

namespace Services.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(sec => sec.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(sec => sec.ProductType.Name))
                .ForMember(dest=> dest.PictureUrl ,option => option.MapFrom(sec=>$"https://localhost:7013/{sec.PictureUrl}"));
              
        }
    }
}
