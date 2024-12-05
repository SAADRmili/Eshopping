using AutoMapper;
using Catalog.Application.Commands;
using Catalog.Application.Responses;
using Catalog.Core.Entites;
using Catalog.Core.Specs;

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
        CreateMap<Product, CreateProductCommand>().ReverseMap();
        CreateMap<Product, UpdateProductCommand>().ReverseMap();
        CreateMap<Pagination<Product>, Pagination<ProductResponse>>().ReverseMap();
    }

    private void ConfigureTypes()
    {
        CreateMap<ProductType, TypeReponse>().ReverseMap();
    }
}
