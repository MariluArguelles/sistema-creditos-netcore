using AutoMapper;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Mappers
{
    public class CategoryMappingsProfile : Profile
    {
        public CategoryMappingsProfile()
        {
            CreateMap<Category, CategoryResponseDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate.Value.ToShortDateString()))
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDto>>()
                .ReverseMap();

            CreateMap<CategoryRequestDto, Category>();

            CreateMap<Category, CategorySelectResponseDto>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ReverseMap();

        }
    }

}
