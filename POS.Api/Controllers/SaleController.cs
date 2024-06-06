using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Sales.Request;
using POS.Application.ExportPDF;
using POS.Application.ExportPDF.Report;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Utilities.Statics;
using System.Data;
using static Google.Apis.Requests.BatchRequest;


namespace POS.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : Controller
    {
        private readonly ISaleApplication _saleApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;
        private readonly IGeneratePDF _generatePDF;
        private readonly IReport _report;
        private readonly IMapper _mapper;

        public SaleController(ISaleApplication saleApplication,

          IGenerateExcelApplication generateExcelApplication, IGeneratePDF generatePDF, IMapper mapper, IReport report)
        {
            _saleApplication = saleApplication;
            _generateExcelApplication = generateExcelApplication;
            _generatePDF = generatePDF;
            _report = report;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListSales(int customerId, [FromQuery] BaseFiltersRequest filters)
        {
            var response = await _saleApplication.ListSales(customerId, filters);
            return Ok(response);
        }

        [HttpGet("ListSalesForPayments")]
        public async Task<IActionResult> ListSalesForPayments(int customerId, [FromQuery] BaseFiltersRequest filters)
        {
            var response = await _saleApplication.ListSalesForPayments(customerId, filters);
            return Ok(response);
        }

        [HttpGet("{saleId:int}")]
        public async Task<IActionResult> SaleById(int saleId)
        {
            var response = await _saleApplication.SaleById(saleId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterSale([FromBody] SaleRequestDto requestDto)
        {
            var response = await _saleApplication.RegisterSale(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{saleId:int}")]
        public async Task<IActionResult> EditSale(int saleId, [FromBody] SaleRequestDto requestDto)
        {
            var response = await _saleApplication.EditSale(saleId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{saleId:int}")]
        public async Task<IActionResult> RemoveSale(int saleId)
        {
            var response = await _saleApplication.RemoveSale(saleId);
            return Ok(response);
        }

        [HttpGet("GetBill/{saleId:int}")]
        public async Task<ActionResult> GetBill(int saleId)
        {
            var fileBytes = new MemoryStream();
            try
            {
                List<DataTable> lis = new List<DataTable>();
                lis = await _saleApplication.GetBill(saleId);

                fileBytes = await _report.GenerateBill(saleId, lis);

                return File(fileBytes, ContentType.ContentTypePdf);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
