using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Dtos.Payment.Request;
using POS.Application.Dtos.Payment.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface IPaymentApplication
    {
        Task<BaseResponse<BaseEntityResponse<PaymentResponseDto>>> ListPayments(BaseFiltersRequest filters);
        //Task<DataTable> ListCategoriesDataTable(BaseFiltersRequest filters);  //para pdf
        Task<BaseResponse<bool>> RegisterPayment(PaymentRequestDto requestDto);
        Task<BaseResponse<bool>> RemovePayment(int paymentId);
    }
}
