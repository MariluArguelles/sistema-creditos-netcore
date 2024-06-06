using AutoMapper;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.Payment.Request;
using POS.Application.Dtos.Payment.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;

namespace POS.Application.Mappers
{
    public class PaymentMappingsProfile : Profile
    {
        public PaymentMappingsProfile()
        {
            CreateMap<Payment, PaymentResponseDto>()
                 .ForMember(x => x.PaymentId, x => x.MapFrom(y => y.Id))
                 .ReverseMap();

            CreateMap<BaseEntityResponse<Payment>, BaseEntityResponse<PaymentResponseDto>>()
                .ReverseMap();

            CreateMap<PaymentRequestDto, Payment>();

            

        }
    }
}
