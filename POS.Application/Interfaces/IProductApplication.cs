using POS.Application.Commons.Bases;
using POS.Application.Dtos.Product.Request;
using POS.Application.Dtos.Product.Response;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using System.Data;

namespace POS.Application.Interfaces
{
    public interface IProductApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProductResponseDto>>> ListProducts(BaseFiltersRequest filters);
        Task<DataTable> ListProductsDataTable(BaseFiltersRequest filters);  //para pdf
        Task<BaseResponse<ProductResponseDto>> ProductById(int productId);
        Task<BaseResponse<bool>> RegisterProduct(ProductRequestDto requestDto);
        Task<BaseResponse<bool>> EditProduct(int productId, ProductRequestDto requestDto);
        Task<BaseResponse<bool>> RemoveProduct(int productId);
    }
}
