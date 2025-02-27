using AutoMapper;
using Sales.Application.Commands.Products;
using Sales.Domain.Entities;

namespace Sales.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductCommand, Product>().ReverseMap();
        }
    }
}