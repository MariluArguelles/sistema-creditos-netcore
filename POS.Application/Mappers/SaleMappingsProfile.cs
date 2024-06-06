using AutoMapper;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Dtos.Sales.Request;
using POS.Application.Dtos.Sales.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;

namespace POS.Application.Mappers
{
    public class SaleMappingsProfile : Profile
    {
        public SaleMappingsProfile()
        {
            //Lista de productos con filtro
            CreateMap<Sale, SaleResponseDto>()
              .ForMember(x => x.saleId, x => x.MapFrom(y => y.Id))
              .ForMember(x => x.Total, x => x.MapFrom(y =>  Convert.ToDouble(y.Total).ToString() ))
              //.ForMember(x => x.Paid, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Si" : "No"))
              .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate.Value.ToShortDateString()))
              .ReverseMap();

            //no funciona el mappiung por que no meti las entidades de payments y saleItems
            //no las meti por que saleItems Product genera una dependencia circular a SaleItems
            //CreateMap<Sale, SaleForPaymentsResponseDto>()
            // .ForMember(x => x.Total, x => x.MapFrom(y => y.Total))
            // .ForMember(x => x.CustomerId, x => x.MapFrom(y => y.CustomerId))

            //  .ForMember(x => x.SubTotal, x => x.MapFrom(y => y.SubTotal))
            //   .ForMember(x => x.Balance, x => x.MapFrom(y => y.Balance))
            //    .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate))
            // .ForMember(x => x.SaleItems, x => x.MapFrom(y => y.SaleItems.Select(si => new SaleItemResponse
            // {
            //     Id = si.Id,
            //     Quantity = si.Quantity,
            //     Price = si.Price,
            //     Description = si.Product.Description.Trim(),
            // }).ToList()))
            // .ForMember(x => x.Payments, x => x.MapFrom(y => y.Payments.Select(p => new PaymentResponse
            // {
            //     Quantity = p.Quantity,
            //     Balance = p.Balance,
            //     PaymentDate = p.PaymentDate
            // }).ToList()))
            //    .ForMember(x => x.AuditDeleteUser, x => x.MapFrom(y => y.AuditDeleteUser))
            //       .ForMember(x => x.AuditDeleteDate, x => x.MapFrom(y => y.AuditDeleteDate))
            //          .ForMember(x => x.CustomerId, x => x.MapFrom(y => y.CustomerId))
            //.ReverseMap();


            CreateMap<BaseEntityResponse<Sale>, BaseEntityResponse<SaleResponseDto>>()
                    .ReverseMap();
            //registrar
            CreateMap<SaleRequestDto, Sale>();
        }
    }
}
