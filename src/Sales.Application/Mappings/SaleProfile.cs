using AutoMapper;
using Sales.Application.Commands.Sales;
using Sales.Application.DTOs;
using Sales.Domain.Entities;

namespace Sales.Application.Mappings
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SaleDate));

            CreateMap<SaleItemCommand, SaleItem>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<SaleItem, SaleItemDto>().ReverseMap();
        }
    }
}