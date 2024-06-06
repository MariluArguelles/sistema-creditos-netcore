using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Payment.Request;
using POS.Application.ExportPDF;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Utilities.Statics;

namespace POS.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentApplication _paymentApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;
        private readonly IGeneratePDF _generatePDF;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentApplication paymentApplication, IGenerateExcelApplication generateExcelApplication, IGeneratePDF generatePDF, IMapper mapper)
        {
            _paymentApplication = paymentApplication;
            _generateExcelApplication = generateExcelApplication;
            _generatePDF = generatePDF;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListPayments([FromQuery] BaseFiltersRequest filters)
        {
            var columnNames = ExcelColumnNames.GetColumnsCategories();
            var response = await _paymentApplication.ListPayments(filters);

            if ((bool)filters.Download!)
            {
                var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }
            //if (filters.Download == "Pdf")
            //{
            //    DataTable tabla = await _categoryApplication.ListCategoriesDataTable(filters);
            //    var fileBytes2 = _generatePDF.GenerateToPDF(tabla, columnNames);
            //    return File(fileBytes2, ContentType.ContentTypePdf);
            //}
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterPayment([FromBody] PaymentRequestDto requestDto)
        {
            var response = await _paymentApplication.RegisterPayment(requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{paymentId:int}")]
        public async Task<IActionResult> RemovePayment(int paymentId)
        {
            var response = await _paymentApplication.RemovePayment(paymentId);
            return Ok(response);
        }





    }
}
