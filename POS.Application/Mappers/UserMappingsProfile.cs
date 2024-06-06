using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.User.Request;
using POS.Application.Dtos.User.Response;
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
    public class UserMappingsProfile:Profile
    {
        public UserMappingsProfile()
        {
            CreateMap<UserRequestDto, User>();

            CreateMap<UserUpdateRequestDto, User>();

            CreateMap<TokenRequestDto, User>();

            CreateMap<User, UserResponseDto>()
                .ForMember(x => x.UserId, x => x.MapFrom(y => y.Id))
                 .ForMember(x => x.AuditCreateDate, x => x.MapFrom(y => y.AuditCreateDate.Value.ToShortDateString()))
                 .ForMember(x => x.StateUser, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();


                CreateMap<BaseEntityResponse<User>, BaseEntityResponse<UserResponseDto>>()
                    .ReverseMap();

                CreateMap<CategoryRequestDto, User>();

            
        }
    }
}
