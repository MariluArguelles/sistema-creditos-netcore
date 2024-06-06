using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.Customer.Request;
using POS.Application.Dtos.Customer.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;

namespace POS.Application.Mappers
{
    public class CustomerMappingsProfile : Profile
    {

        public CustomerMappingsProfile()
        {
            // tranformaciòn de los objetos
            CreateMap<Customer, CustomerResponseDto>()
              .ForMember(x => x.CustomerId, x=> x.MapFrom(y => y.Id))
              .ForMember(x => x.GenderText, x => x.MapFrom(y =>  Convert.ToInt32(y.Gender).Equals((int)Gender.Hombre) ? "Hombre" : "Mujer"))
              .ForMember(x => x.BirthDate, x => x.MapFrom(y => y.BirthDate.Value.ToShortDateString()))// cuando tiene ? en Customer
              .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate.Value.ToShortDateString()))
              .ForMember(x => x.StateCustomer, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
               .ReverseMap();

            CreateMap<BaseEntityResponse<Customer>, BaseEntityResponse<CustomerResponseDto>>()
            
                .ReverseMap();

            CreateMap<CustomerRequestDto, Customer>();//Registrar cliente

            CreateMap<Customer, CustomerSelectResponseDto>()
              .ForMember(x => x.CustomerId, x => x.MapFrom(y => y.Id))
                .ReverseMap();//funciona.

        }

    }
}
