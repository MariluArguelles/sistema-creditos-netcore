using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Customer.Request;
using POS.Application.Dtos.Customer.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Customer;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Statics;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace POS.Application.Services
{

    public class CustomerApplication : ICustomerApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CustomerValidator _validationRules;

        public CustomerApplication(IUnitOfWork unitOfWork, IMapper mapper, CustomerValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }
        
        public async Task<BaseResponse<BaseEntityResponse<CustomerResponseDto>>> ListCustomers(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CustomerResponseDto>>();
            try { 
                        
            var customers = await _unitOfWork.Customer.ListCustomers(filters);

            if (customers is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<CustomerResponseDto>>(customers);
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

        public async Task<BaseResponse<IEnumerable<CustomerSelectResponseDto>>> ListSelectCustomers()
        {
            var response = new BaseResponse<IEnumerable<CustomerSelectResponseDto>>();
            try { 

            var customers = await _unitOfWork.Customer.GetAllAsync();

            if (customers is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<CustomerSelectResponseDto>>(customers);
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
        public async Task<BaseResponse<CustomerResponseDto>> CustomerById(int customerId)
        {
            var response = new BaseResponse<CustomerResponseDto>();
            try{ 
            var customer = await _unitOfWork.Customer.GetByIdAsync(customerId);
            if (customer is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<CustomerResponseDto>(customer);
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


        public async Task<BaseResponse<bool>> RegisterCustomer(CustomerRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try { 
            var validationResult = await _validationRules.ValidateAsync(requestDto);

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_VALIDATE;
                response.Errors = validationResult.Errors;
                return response;
            }
            var customer = _mapper.Map<Customer>(requestDto);
            response.Data = await _unitOfWork.Customer.RegisterAsync(customer);

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
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> EditCustomer(int customerId, CustomerRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try { 

            var customerEdit = await CustomerById(customerId);

            if (customerEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }

            var customer = _mapper.Map<Customer>(requestDto);
            customer.Id = customerId;
            response.Data = await _unitOfWork.Customer.EditAsync(customer);

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

      
        public async Task<BaseResponse<bool>> RemoveCustomer(int customerId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var customer = await CustomerById(customerId);

            if (customer.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
            }
            response.Data = await _unitOfWork.Customer.RemoveAsync(customerId);

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

        public async Task<DataTable> ListCustomersDataTable(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CustomerResponseDto>>();
            DataTable tabla=new DataTable();
            try
            {
                var customers = await _unitOfWork.Customer.ListCustomers(filters);
                
                if (customers is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<CustomerResponseDto>>(customers);
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

        
        private DataTable ConvertToDataTable(List<CustomerResponseDto> data)
        {
            DataTable dataTable = new DataTable();
            Type type = typeof(CustomerResponseDto);
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
            foreach (CustomerResponseDto item in data)
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
