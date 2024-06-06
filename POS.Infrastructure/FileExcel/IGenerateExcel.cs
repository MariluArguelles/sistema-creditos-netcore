using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Statics;

namespace POS.Infrastructure.FileExcel
{
    public interface IGenerateExcel
    {
        MemoryStream GenerateToExcel<T>(BaseEntityResponse<T> data,List<TableColumn> columns);
        
    }
}
