using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.SaleItem.Request;
using POS.Application.ExportPDF;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleItemController : Controller
    {
        private readonly ISaleItemApplication _saleItemApplication;
        private readonly IMapper _mapper;
        public SaleItemController(ISaleItemApplication saleItemApplication,

            IGenerateExcelApplication generateExcelApplication, IGeneratePDF generatePDF, IMapper mapper)
        {
            _saleItemApplication = saleItemApplication;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListSaleItems(int saleId, [FromQuery] BaseFiltersRequest filters)
        {
            var response = await _saleItemApplication.ListSaleItems(saleId, filters);

            return Ok(response);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterSaleItem([FromBody] SaleItemRequestDto requestDto)
        {
            var response = await _saleItemApplication.RegisterSaleItem(requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{saleItemId:int}")]
        public async Task<IActionResult> RemoveSale(int saleItemId)
        {
            var response = await _saleItemApplication.RemoveSaleItem(saleItemId);
            return Ok(response);
        }

        //Task<BaseResponse<BaseEntityResponse<SaleItemResponseDto>>> ListSaleItems(int saleId, BaseFiltersRequest filters);
        //Task<BaseResponse<bool>> RegisterSaleItem(SaleItemResponseDto requestDto);
        //Task<BaseResponse<bool>> RemoveSaleItem(int saleItemId);
    }
}
