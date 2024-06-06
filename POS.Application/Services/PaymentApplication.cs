using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.Payment.Request;
using POS.Application.Dtos.Payment.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Customer;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Services
{
    public class PaymentApplication : IPaymentApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PaymentValidator _validationRules; //validaciones del objeto categoría

        public PaymentApplication(IUnitOfWork unitOfWork, IMapper mapper, PaymentValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        [HttpPost]
        public async Task<BaseResponse<BaseEntityResponse<PaymentResponseDto>>> ListPayments(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<PaymentResponseDto>>();
            try
            {
                var categories = await _unitOfWork.Payment.ListPayments(filters);
                if (categories is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<PaymentResponseDto>>(categories);
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
                response.Message = ex.Message;
                 WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RegisterPayment(PaymentRequestDto requestDto)
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
                var payment = _mapper.Map<Payment>(requestDto);
                response.Data = await _unitOfWork.Payment.RegisterAsync(payment);

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
                response.Message = ex.Message;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }
        

        public async Task<BaseResponse<bool>> RemovePayment(int paymentId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                //var category = await CategoryById(categoryId);

                //if (category.Data is null)
                //{
                //    response.IsSuccess = false;
                //    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                //    return response;
                //}
                response.Data = await _unitOfWork.Payment.RemoveAsync(paymentId);

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
                response.Message = ex.Message;
                WatchDog.WatchLogger.Log(ex.Message);
            }
            return response;
        }
    }
}
