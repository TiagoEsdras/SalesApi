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
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.SaleDate))
                .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.GetTotalAmount()));

            CreateMap<SaleItemCommand, SaleItem>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.TotalPrice))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount));

            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<SaleItem, SaleItemDto>().ReverseMap();
        }
    }
}