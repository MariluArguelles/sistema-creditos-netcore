using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Customer.Request;
using POS.Application.ExportPDF;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Utilities.Statics;
using System.Data;

namespace POS.Api.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;
        private readonly IGeneratePDF _generatePDF;
        //private readonly IGeneratePdfApplication _generatePDFApplication;

        public CustomerController(ICustomerApplication customerApplication, 
            IGenerateExcelApplication generateExcelApplication,IGeneratePDF generatePDF)
        {
            _customerApplication = customerApplication;
            _generateExcelApplication = generateExcelApplication;
            _generatePDF = generatePDF;
        }


        [HttpGet]
        public async Task<IActionResult> ListCustomers([FromQuery] BaseFiltersRequest filters)
        {
            var columnNames = ExcelColumnNames.GetColumnsCustomers();

            var response = await _customerApplication.ListCustomers(filters);

            if ((bool)filters.Download! && filters.DownloadType == "Excel")
            {
                var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            if ((bool)filters.Download! && filters.DownloadType == "PDF")
            {

                DataTable tabla = await _customerApplication.ListCustomersDataTable(filters);
                var fileBytes2 = _generatePDF.GenerateToPDF(tabla, columnNames, "clientes");
                return File(fileBytes2, ContentType.ContentTypePdf);
            }

            return Ok(response);
        }
      
        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectCustomers()
        {
            var response = await _customerApplication.ListSelectCustomers();
            return Ok(response);
        }

        [HttpGet("{customerId:int}")]
        public async Task<IActionResult> CustomerById(int customerId)
        {
            var response = await _customerApplication.CustomerById(customerId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCustomer([FromBody] CustomerRequestDto requestDto)
        {
            var response = await _customerApplication.RegisterCustomer(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{customerId:int}")]
        public async Task<IActionResult> EditCustomer(int customerId, [FromBody] CustomerRequestDto requestDto)
        {
            var response = await _customerApplication.EditCustomer(customerId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{customerId:int}")]
        public async Task<IActionResult> RemoveCustomer(int customerId)
        {
            var response = await _customerApplication.RemoveCustomer(customerId);
            return Ok(response);
        }

    }

}
