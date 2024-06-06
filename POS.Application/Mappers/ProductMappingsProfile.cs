using AutoMapper;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;

namespace POS.Application.Mappers
{
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile() {
            //Lista de productos con filtro
            CreateMap<Product, ProductResponseDto>()
              .ForMember(x => x.ProductId, x => x.MapFrom(y => y.Id))
              .ForMember(x => x.AuditCreateDate, x=> x.MapFrom(y => y.AuditCreateDate.Value.ToShortDateString()))
              
                .ForMember(x => x.StateProduct, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();
            
            CreateMap<BaseEntityResponse<Product>, BaseEntityResponse<ProductResponseDto>>()
                    .ReverseMap();
            //registrar
            CreateMap<ProductRequestDto, Product>();
        }
    }
}
