using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Data;

namespace POS.Application.ExportPDF
{

    public class GeneratePDF : IGeneratePDF
        {
        //https://www.youtube.com/watch?v=T5M-b2xXLGw&t=471s   
        public MemoryStream GenerateToPDF(DataTable dt, List<(string ColumnName, string PropertyName)> columnas,string titulo)
            {
            //QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            
            Document document = Document.Create(contenedor =>
                {
                    contenedor.Page(pagina =>
                    {
                        pagina.Size(PageSizes.A4);
                        pagina.Margin(1, QuestPDF.Infrastructure.Unit.Centimetre);
                        pagina.Header().Text("Lista de "+ titulo)
                        .Bold().FontSize(25).FontColor(Colors.Black);


                        pagina.Content().Table(table =>
                        {
                            try
                            {
                                table.ColumnsDefinition(columns =>
                                { for (int i = 0; i < columnas.Count; i++) columns.RelativeColumn(); });

                                table.Header(header =>
                                {
                                    //Colors.Grey.Lighten2
                                    for (int i = 0; i < columnas.Count; i++) header.Cell()
                                        .Border(1).BorderColor(Colors.Blue.Lighten5).Background(Colors.Blue.Accent4).Padding(5)
                                        .Text(columnas[i].ColumnName).FontColor(Colors.White);
                                });

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    foreach (var col in columnas)
                                    {
                                        if (col.PropertyName == "BirthDate")
                                            table.Cell().Border(1).BorderColor(Colors.Blue.Lighten5)
                                                .Background((i % 2 == 0) ? Colors.LightBlue.Lighten5 : Colors.Yellow.Lighten5)
                                                .Text(Convert.ToDateTime(dt.Rows[i][col.PropertyName]).ToString("dd/MM/yyyy")).Light();

                                        else if (col.PropertyName == "AuditCreateDate")
                                            table.Cell().Border(1).BorderColor(Colors.Blue.Lighten5)
                                                .Background((i % 2 == 0) ? Colors.LightBlue.Lighten5 : Colors.Yellow.Lighten5)
                                                //.Text(Convert.ToDateTime(dt.Rows[i][col.PropertyName]).ToString("dd/MM/yyyy HH:mm")).Light();
                                                .Text(Convert.ToDateTime(dt.Rows[i][col.PropertyName]).ToString("dd/MM/yyyy")).Light();

                                        else table.Cell().Border(1).BorderColor(Colors.Blue.Lighten5)
                                            .Background((i % 2 == 0) ? Colors.LightBlue.Lighten5 : Colors.Yellow.Lighten5)
                                            .Text(dt.Rows[i][col.PropertyName].ToString()).Light();
                                    }
                                }
                            }
                            catch (Exception ex) 
                            { 
                            
                            }
                        }
                        );

                        pagina.Footer().AlignCenter().Text(x =>
                        {
                            x.CurrentPageNumber();
                            x.Span(" / ");
                            x.TotalPages();
                        });
                    });

                });

                byte[] pdfBytes = document.GeneratePdf();
                MemoryStream ms = new MemoryStream(pdfBytes);

                return ms;
            }

    }
   

}
