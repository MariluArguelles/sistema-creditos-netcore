using POS.Application.Commons.Bases;
using POS.Application.Dtos.SaleItem.Request;
using POS.Application.Dtos.SaleItem.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;

namespace POS.Application.Interfaces
{
    public interface ISaleItemApplication
    {
        Task<BaseResponse<BaseEntityResponse<SaleItemResponsetDto>>> ListSaleItems(int saleId,BaseFiltersRequest filters);
        Task<BaseResponse<bool>> RegisterSaleItem(SaleItemRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveSaleItem(int saleItemId);
    }
}
