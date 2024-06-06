using POS.Application.Commons.Bases;
using POS.Application.Dtos.Sale.Response;
using POS.Application.Dtos.Sales.Request;
using POS.Application.Dtos.Sales.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using System.Data;

namespace POS.Application.Interfaces
{
    public interface ISaleApplication
    {

        //Sales
        Task<BaseResponse<BaseEntityResponse<SaleResponseDto>>> ListSales(int customerId, BaseFiltersRequest filters);
        Task<BaseResponse<BaseEntityResponse<SaleForPaymentsResponseDto>>> ListSalesForPayments(int customerId, BaseFiltersRequest filters);
        //Task<DataTable> ListProductsDataTable(BaseFiltersRequest filters);  //para pdf
        Task<BaseResponse<SaleResponseDto>> SaleById(int saleId);
        Task<BaseResponse<bool>> RegisterSale(SaleRequestDto requestDto);
        Task<BaseResponse<bool>> EditSale(int saleId, SaleRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveSale(int saleId);

        Task<List<DataTable>> GetBill(int saleId);


    }
}      
