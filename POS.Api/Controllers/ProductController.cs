using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Product.Request;
using POS.Application.ExportPDF;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Utilities.Statics;
using System.Data;

namespace POS.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;
        private readonly IGeneratePDF _generatePDF;
        private readonly IMapper _mapper;

        public ProductController(IProductApplication productApplication,
            
            IGenerateExcelApplication generateExcelApplication, IGeneratePDF generatePDF, IMapper mapper)
        {
            _productApplication = productApplication;
            _generateExcelApplication = generateExcelApplication;
            _generatePDF = generatePDF;
        }

        [HttpGet]
        public async Task<IActionResult> ListProduct([FromQuery] BaseFiltersRequest filters)
        {
            var columnNames = ExcelColumnNames.GetColumnsProducts();
            var response = await _productApplication.ListProducts(filters);

            if ((bool)filters.Download! && filters.DownloadType == "Excel")
            {
                var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            if ((bool)filters.Download! && filters.DownloadType == "PDF")
            {
                DataTable tabla = await _productApplication.ListProductsDataTable(filters);
                var fileBytes2 = _generatePDF.GenerateToPDF(tabla, columnNames,"productos");
                return File(fileBytes2, ContentType.ContentTypePdf);
            }

            return Ok(response);
        }
   

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> ProductById(int productId)
        {
            var response = await _productApplication.ProductById(productId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterProduct([FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.RegisterProduct(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{productId:int}")]
        public async Task<IActionResult> EditProduct(int productId, [FromBody] ProductRequestDto requestDto)
        {
            var response = await _productApplication.EditProduct(productId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{productId:int}")]
        public async Task<IActionResult> RemoveProduct(int productId)
        {
            var response = await _productApplication.RemoveProduct(productId);
            return Ok(response);
        }
    }
}
