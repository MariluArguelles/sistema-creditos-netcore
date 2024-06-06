using POS.Utilities.Statics;
using System.Data;

namespace POS.Application.ExportPDF
{
    public interface IGeneratePDF
    {
        MemoryStream GenerateToPDF(DataTable tabla, List<(string ColumnName, string PropertyName)> columns,string titulo);


    }
}
