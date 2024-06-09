using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Dtos.Sales.Request;
using POS.Application.Dtos.Sales.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;
using System.Globalization;

namespace POS.Application.Mappers
{
    public class SaleMappingsProfile : Profile
    {
        public SaleMappingsProfile()
        {
            //(1)ListSales 
            CreateMap<Sale, SaleResponseDto>()
              .ForMember(x => x.saleId, x => x.MapFrom(y => y.Id))
              .ForMember(x => x.Total, x => x.MapFrom(y => Convert.ToDouble(y.Total).ToString()))
              .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate.Value.ToShortDateString()))
              .ReverseMap();
            CreateMap<BaseEntityResponse<Sale>, BaseEntityResponse<SaleResponseDto>>()
                   .ReverseMap();
            //(1)ListSales 

            //(2) ListSalesForPayments
            //CreateMap<Sale, SaleForPaymentsResponseDto>()
            CreateMap<Sale, SaleForPaymentsResponseDto>()
                .ForMember(dest => dest.saleId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total.ToString("F2")))
                .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal.ToString("F2")))
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance.ToString("F2")))
                //.ForMember(dest => dest.Closed, opt => opt.MapFrom(src => src.Closed))
                //.ForMember(dest => dest.Paid, opt => opt.MapFrom(src => src.Paid))
                .ForMember(dest => dest.AuditCreateDate, opt => opt.MapFrom(src => src.AuditCreateDate.ToString()))
                .ForMember(dest => dest.SaleItems, opt => opt.MapFrom(src => src.SaleItems.Select(si => new SaleItemResponse
                {
                    Id = si.Id,
                    Quantity = si.Quantity,
                    Price = si.Price,
                    Description = si.Product.Description.Trim(),
                }).ToList()))
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments.Select(p => new PaymentResponse
                {
                    Quantity = p.Quantity,
                    Balance = p.Balance,
                    PaymentDate = p.PaymentDate
                }).ToList()))
                .ForMember(dest => dest.AuditDeleteUser, opt => opt.MapFrom(src => src.AuditDeleteUser))
                .ForMember(dest => dest.AuditDeleteDate, opt => opt.MapFrom(src => src.AuditDeleteDate))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

            CreateMap(typeof(BaseEntityResponse<>), typeof(BaseEntityResponse<>));
            //(2) ListSalesForPayments




            //registrar
            CreateMap<SaleRequestDto, Sale>();
        }
    }
}
