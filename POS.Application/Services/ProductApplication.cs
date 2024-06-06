using AutoMapper;
using DocumentFormat.OpenXml.Office2010.Excel;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Customer;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Statics;
using System.Data;
using System.Reflection;

namespace POS.Application.Services
{
    public class ProductApplication : IProductApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductValidator _validationRules; //validaciones del objeto categoría

        public ProductApplication(IUnitOfWork unitOfWork, IMapper mapper, ProductValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }
        public async Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductResponseDto>>();
            try
            {
                var products = await _unitOfWork.Product.ListProducts(filters);
                if (products is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<ProductResponseDto>>(products);
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

        //public async Task<T> GetByIdEntity(int id)
        //{
        //    var getById = await .AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        //    return getById!;
        //}

        public async Task<BaseResponse<ProductResponseDto>> ProductById(int productId)
        {
            var response = new BaseResponse<ProductResponseDto>();
            try
            {
                var product = await _unitOfWork.Product.GetByIdAsync(productId);
                //var product = await GetByIdEntity(productId);

                if (product is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ProductResponseDto>(product);
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

        public async Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto)
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
                var product = _mapper.Map<Product>(requestDto);
                //product.Sku = "";
                response.Data = await _unitOfWork.Product.RegisterAsync(product);

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
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> EditProduct(int productId, ProductRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var productEdit = await ProductById(productId);

                if (productEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                var product = _mapper.Map<Product>(requestDto);
                product.Id = productId;
                response.Data = await _unitOfWork.Product.EditAsync(product);

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

        public async Task<BaseResponse<bool>> RemoveProduct(int productId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var product = await ProductById(productId);

                if (product.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }
                response.Data = await _unitOfWork.Product.RemoveAsync(productId);

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
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<DataTable> ListProductsDataTable(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductResponseDto>>();
            DataTable tabla = new DataTable();
            try
            {
                var products = await _unitOfWork.Product.ListProducts(filters);

                if (products is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<ProductResponseDto>>(products);
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


        private DataTable ConvertToDataTable(List<ProductResponseDto> data) 
        {
            DataTable dataTable = new DataTable();
            Type type = typeof(ProductResponseDto);
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
            foreach (ProductResponseDto item in data)
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
