﻿using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.FileExcel;
using POS.Utilities.Statics;
using WatchDog;

namespace POS.Application.Services
{
    public class GenerateExcelApplication : IGenerateExcelApplication
    {
        private readonly IGenerateExcel _generateExcel;

        public GenerateExcelApplication(IGenerateExcel generateExcel) { 
            _generateExcel = generateExcel;
        }

        public byte[] GenerateToExcel<T>(BaseEntityResponse<T> data, List<(string ColumnName, string PropertyName)> columns)
        {
                var excelColumns = ExcelColumnNames.GetColumns(columns);
                var memoryStreamExcel = _generateExcel.GenerateToExcel(data, excelColumns);
                var fileBytes = memoryStreamExcel.ToArray();
            return fileBytes;
        }
    }
}
