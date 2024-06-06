using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.User.Request;
using POS.Application.ExportPDF;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Utilities.Statics;
using System.Data;

namespace POS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _userApplication;
        private readonly IGenerateExcelApplication _generateExcelApplication;
        private readonly IGeneratePDF _generatePDF;
        

        public UserController(IUserApplication userApplication,IGenerateExcelApplication generateExcelApplication, IGeneratePDF generatePDF, IMapper mapper)
        {
            _userApplication = userApplication;
            _generateExcelApplication = generateExcelApplication;
            _generatePDF = generatePDF;
        }


        [HttpGet]
        public async Task<IActionResult> ListUsers([FromQuery] BaseFiltersRequest filters)
        {
            var columnNames = ExcelColumnNames.GetColumnsUsers();
            var response = await _userApplication.ListUsers(filters);

            if (response.Data is not null)
            {
                if ((bool)filters.Download! && filters.DownloadType == "Excel")
                {
                    var fileBytes = _generateExcelApplication.GenerateToExcel(response.Data!, columnNames);
                    return File(fileBytes, ContentType.ContentTypeExcel);
                }

                if ((bool)filters.Download! && filters.DownloadType == "PDF")
                {
                    DataTable tabla = await _userApplication.ListUsersDataTable(filters);
                    var fileBytes2 = _generatePDF.GenerateToPDF(tabla, columnNames, "usuarios");
                    return File(fileBytes2, ContentType.ContentTypePdf);
                }
            }

            return Ok(response);
        }

        
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRequestDto requestDto)
        {
            var response = await _userApplication.RegisterUser(requestDto);
            return Ok(response);
        }

        [HttpGet("{userId:int}")]
        public async Task<IActionResult> UserById(int userId)
        {
            var response = await _userApplication.UserById(userId);
            return Ok(response);
        }

        [HttpPut("Edit/{userId:int}")]
        public async Task<IActionResult> EditUser(int userId, [FromBody] UserUpdateRequestDto requestDto)
        {
            var response = await _userApplication.EditUser(userId, requestDto);
            return Ok(response);
        }

    }
}
