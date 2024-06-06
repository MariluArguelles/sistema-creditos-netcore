using AutoMapper;
using POS.Application.Dtos.SaleItem.Request;
using POS.Application.Dtos.SaleItem.Response;
using POS.Application.Dtos.Sales.Request;
using POS.Application.Dtos.Sales.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Mappers
{
    public class SaleItemMappingsProfile : Profile
    {

        public SaleItemMappingsProfile()
        {
            CreateMap<SaleItem, SaleItemResponsetDto>()
          .ForMember(x => x.SaleItemId, x => x.MapFrom(y => y.Id))
          .ForMember(x => x.ProductDescription, x => x.MapFrom(y => y.Product.Description))
          .ForMember(x => x.Price, x => x.MapFrom(y => y.Price))
          .ReverseMap();


            CreateMap<BaseEntityResponse<SaleItem>, BaseEntityResponse<SaleItemResponsetDto>>()
                        .ReverseMap();
            //registrar
            CreateMap<SaleItemRequestDto, SaleItem>();
        }
        
    }
}
