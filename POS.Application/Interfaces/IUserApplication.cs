using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.User.Request;
using POS.Application.Dtos.User.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using System.Data;

namespace POS.Application.Interfaces
{
    public interface IUserApplication
    {
        Task<BaseResponse<BaseEntityResponse<UserResponseDto>>> ListUsers(BaseFiltersRequest filters);

        Task<DataTable> ListUsersDataTable(BaseFiltersRequest filters);

        Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto);
        
        Task<BaseResponse<bool>> EditUser(int userId,UserUpdateRequestDto requestDto);

        Task<BaseResponse<UserResponseDto>> UserById(int userId);
        
    }
}
