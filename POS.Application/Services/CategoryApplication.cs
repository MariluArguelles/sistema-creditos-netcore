using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
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


namespace POS.Application.Services
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CategoryValidator _validationRules; //validaciones del objeto categoría
        
        public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }
        public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDto>>> ListCategories(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CategoryResponseDto>>();
            try
            {
                var categories = await _unitOfWork.Category.ListCategories(filters);
                if (categories is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<CategoryResponseDto>>(categories);
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
                WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<IEnumerable<CategorySelectResponseDto>>> ListSelectCategories()
        {
            var response = new BaseResponse<IEnumerable<CategorySelectResponseDto>>();
            try
            {
                var categories = await _unitOfWork.Category.GetAllAsync();
                if (categories is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDto>>(categories);
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
                WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<CategoryResponseDto>> CategoryById(int categoryId)
        {
            var response = new BaseResponse<CategoryResponseDto>();
            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(categoryId);

                if (category is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<CategoryResponseDto>(category);
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
                WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var validationResult = await _validationRules.ValidateAsync(requestDto);
                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }
                var category = _mapper.Map<Category>(requestDto);
                response.Data = await _unitOfWork.Category.RegisterAsync(category);

                if (response.Data)
                {
                    response.IsSuccess = true; //error? IsSucces a IsSuccess
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

        public async Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var categoryEdit = await CategoryById(categoryId);

                if (categoryEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                var category = _mapper.Map<Category>(requestDto);
                category.Id = categoryId;
                response.Data = await _unitOfWork.Category.EditAsync(category);

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
                WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveCategory(int categoryId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var category = await CategoryById(categoryId);

                if (category.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                response.Data = await _unitOfWork.Category.RemoveAsync(categoryId);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
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

        public async Task<DataTable> ListCategoriesDataTable(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CategoryResponseDto>>();
            DataTable tabla = new DataTable();
            try
            {
                var categories = await _unitOfWork.Category.ListCategories(filters);

                if (categories is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<CategoryResponseDto>>(categories);
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
                WatchLogger.Log(ex.Message);
            }
            return tabla;

        }


        private DataTable ConvertToDataTable(List<CategoryResponseDto> data)
        {
            DataTable dataTable = new DataTable();
            Type type = typeof(CategoryResponseDto);
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
            foreach (CategoryResponseDto item in data)
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
