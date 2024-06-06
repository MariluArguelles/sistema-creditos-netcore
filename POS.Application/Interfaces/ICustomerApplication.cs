using POS.Application.Commons.Bases;
using POS.Application.Dtos.Customer.Request;
using POS.Application.Dtos.Customer.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using System.Data;

namespace POS.Application.Interfaces
{
    public interface ICustomerApplication
    {
        Task<BaseResponse<BaseEntityResponse<CustomerResponseDto>>> ListCustomers(BaseFiltersRequest filters);

        Task<DataTable> ListCustomersDataTable(BaseFiltersRequest filters);  //para pdf

        Task<BaseResponse<IEnumerable<CustomerSelectResponseDto>>> ListSelectCustomers();
        Task<BaseResponse<CustomerResponseDto>> CustomerById(int customerId);
        Task<BaseResponse<bool>> RegisterCustomer(CustomerRequestDto  requestDto);
        Task<BaseResponse<bool>> EditCustomer(int customerId,CustomerRequestDto requestDto);

        Task<BaseResponse<bool>> RemoveCustomer(int customerId);
        
    }
}
