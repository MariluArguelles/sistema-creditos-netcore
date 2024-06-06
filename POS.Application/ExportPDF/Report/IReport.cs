using System.Data;

namespace POS.Application.ExportPDF.Report
{
    public interface IReport
    {
       public Task<MemoryStream> GenerateBill(int idSale,List<DataTable> list);
        
    }
}
