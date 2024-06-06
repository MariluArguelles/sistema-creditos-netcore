using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.SaleItem.Request;
using POS.Application.Dtos.SaleItem.Response;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Interfaces;
using POS.Utilities.Statics;

namespace POS.Application.Services
{
    public class SaleItemApplication : ISaleItemApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SaleItemApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //  _validationRules = validationRules;
        }
        public async Task<BaseResponse<BaseEntityResponse<SaleItemResponsetDto>>> ListSaleItems(int saleId, BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<SaleItemResponsetDto>>();
            try
            {
                var sales = await _unitOfWork.SaleItem.ListSaleItems(saleId,filters);
                if (sales is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<SaleItemResponsetDto>>(sales);
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

        public async Task<BaseResponse<bool>> RegisterSaleItem(SaleItemRequestDto requestDto)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var sale = _mapper.Map<SaleItem>(requestDto);
                //product.Sku = "";
                response.Data = await _unitOfWork.SaleItem.RegisterAsync(sale);

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

        public async Task<BaseResponse<bool>> RemoveSaleItem(int saleItemId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                //var sale = await SaleById(saleId);

                //if (sale.Data is null)
                //{
                //    response.IsSuccess = false;
                //    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                //    return response;
                //}
                response.Data = await _unitOfWork.SaleItem.RemoveAsync(saleItemId);

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
    }
}
