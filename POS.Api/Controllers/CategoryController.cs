using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Category.Request;
using POS.Application.ExportPDF;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Utilities.Statics;
using System.Data;

namespace POS.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication _categoryApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;
        private readonly IGeneratePDF _generatePDF;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryApplication categoryApplication, IGenerateExcelApplication generateExcelApplication, IGeneratePDF generatePDF, IMapper mapper)
        {
            _categoryApplication = categoryApplication;
            _generateExcelApplication = generateExcelApplication;
            _generatePDF = generatePDF;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> ListCategories([FromQuery] BaseFiltersRequest filters)
        {
            var columnNames = ExcelColumnNames.GetColumnsCategories();
            var response = await _categoryApplication.ListCategories(filters);

            if ((bool)filters.Download! && filters.DownloadType == "Excel")
            {
                var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnNames);
                return File(fileBytes, ContentType.ContentTypeExcel);
            }

            if ((bool)filters.Download! && filters.DownloadType == "PDF")
            {
                DataTable tabla = await _categoryApplication.ListCategoriesDataTable(filters);
                var fileBytes2 = _generatePDF.GenerateToPDF(tabla, columnNames,"categorías");
                return File(fileBytes2, ContentType.ContentTypePdf);
            }

            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectecCategories()
        {//Regresa todas las categorías
            var response = await _categoryApplication.ListSelectCategories();
            return Ok(response);
        }

        [HttpGet("{categoryId:int}")]
        public async Task<IActionResult> CategoryById(int categoryId)
        {
            var response = await _categoryApplication.CategoryById(categoryId);
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterCategory([FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.RegisterCategory(requestDto);
            return Ok(response);
        }

        [HttpPut("Edit/{categoryId:int}")]
        public async Task<IActionResult> EditCategory(int categoryId, [FromBody] CategoryRequestDto requestDto)
        {
            var response = await _categoryApplication.EditCategory(categoryId, requestDto);
            return Ok(response);
        }

        [HttpPut("Remove/{categoryId:int}")]
        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            var response = await _categoryApplication.RemoveCategory(categoryId);
            return Ok(response);
        }
    }

}
