using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.User.Request;
using POS.Application.Dtos.User.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Customer;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Statics;
using System.Data;
using System.Reflection;
using WatchDog;
using BC = BCrypt.Net.BCrypt;


namespace POS.Application.Services
{
    public class UserApplication : IUserApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserValidator _validationRules; //validaciones del objeto categoría

        public UserApplication(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

      
        public async Task<BaseResponse<bool>> RegisterUser(UserRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var account = _mapper.Map<User>(requestDto);
                account.Password = BC.HashPassword(account.Password);
                response.Data = await _unitOfWork.User.RegisterAsync(account);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }
            return response;
        }


        public async Task<BaseResponse<bool>> EditUser(int userId, UserUpdateRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var userEdit = await UserById(userId);

                if (userEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                userEdit.Data.UserName = requestDto.UserName!;
                userEdit.Data.Email = requestDto.Email!;
                userEdit.Data.State = (int)requestDto.State!;

                var user = _mapper.Map<User>(userEdit.Data);

                user.Id = userId;
                response.Data = await _unitOfWork.User.EditAsync(user);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<UserResponseDto>> UserById(int userId)
        {
            var response = new BaseResponse<UserResponseDto>();
            try
            {
                var user = await _unitOfWork.User.GetByIdAsync(userId);

                if (user is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<UserResponseDto>(user);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<BaseEntityResponse<UserResponseDto>>> ListUsers(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<UserResponseDto>>();
            try
            {
                
                var users = await _unitOfWork.User.ListUsers(filters);
                if (users is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<UserResponseDto>>(users);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<DataTable> ListUsersDataTable(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<UserResponseDto>>();
            DataTable tabla = new DataTable();
            try
            {
                var users = await _unitOfWork.User.ListUsers(filters);

                if (users is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<UserResponseDto>>(users);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
                tabla = ConvertToDataTable(response.Data.Items.ToList());
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return tabla;

        }


        private DataTable ConvertToDataTable(List<UserResponseDto> data)
        {
            DataTable dataTable = new DataTable();
            Type type = typeof(UserResponseDto);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                Type propertyType = property.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = Nullable.GetUnderlyingType(propertyType);
                }
                dataTable.Columns.Add(property.Name, propertyType);
            }

            foreach (UserResponseDto item in data)
            {
                DataRow row = dataTable.NewRow();
                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(item);
                    if (value == null)
                    {
                        row[property.Name] = DBNull.Value;
                    }
                    else
                    {
                        row[property.Name] = value;
                    }
                }
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }


    }
}
