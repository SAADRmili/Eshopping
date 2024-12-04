using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Core.Entites;

namespace Catalog.Application.Mappers;
public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        ConfigureBrands();
        ConfigureProducts();
        ConfigureTypes();
    }

    private void ConfigureBrands()
    {
        CreateMap<ProductBrand, BrandsResponse>().ReverseMap();
    }

    private void ConfigureProducts()
    {
        CreateMap<Product, ProductResponse>().ReverseMap();
    }

    private void ConfigureTypes()
    {
        CreateMap<ProductType, TypeReponse>().ReverseMap();
    }
}
