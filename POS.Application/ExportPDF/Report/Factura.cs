using POS.Application.ExportPDF.Report;
using POS.Application.Interfaces;
using POS.Application.Services;
using POS.Domain.Entities;
using QuestPDF.Fluent;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace POS.Application.ExportPDF.Reportes
{
    public class Factura : IReport
    {
        private readonly IUtilidades _utilidades;

        public Factura(IUtilidades utilidades)
        {
            _utilidades = utilidades;
        }

        public async Task<MemoryStream> GenerateBill(int idSale, List<DataTable> list)
        {

            bool isLocalHost = _utilidades.GetFullUrl();
            //QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;// se fue a program.cs
            Document document = Document.Create(contenedor =>
            {
                DataTable dtSale = list[0].Rows[0].Table;
                DataTable dtCustomer = list[1].Rows[0].Table;
                DataTable dtSaleItems = list[2].Rows[0].Table;
                var totalVenta = dtSale.Rows[0]["Total"].ToString();

                contenedor.Page(page =>
                {
                    page.Margin(30);

                    page.Header().ShowOnce().Row(row =>
                    {
                        if (isLocalHost) row.ConstantItem(100).Image("../POS.Application/ExportPDF/Report/logo.png");
                        else row.ConstantItem(100).Image("../Images/ExportPDF/Report/logo.png");


                        row.RelativeItem().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Sistema de Venta 2024").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("Navojoa, Sonora - Mexico").FontSize(9);
                            col.Item().AlignCenter().Text("01 800 547825").FontSize(9);
                            col.Item().AlignCenter().Text("doña@gmail.com").FontSize(9);
                        });

                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Border(1).BorderColor("#257272")
                            .AlignCenter().Text("RFC AGDFD3423423").Bold();

                            col.Item().Background("#257272")
                            .Border(1).BorderColor("#257272").AlignCenter()
                            .Text("Nota de venta").FontColor("#fff");

                            col.Item().Border(1)
                            .BorderColor("#257272").AlignCenter().Text("AGDFD3 - 423423").Bold();
                        });

                    });

                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        col1.Item().Column(col2 => {

                            col2.Item().Text("Datos del cliente").Underline().Bold();

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Nombre: ").SemiBold().FontSize(10);
                                txt.Span(dtCustomer.Rows[0]["Name"].ToString() + " " + dtCustomer.Rows[0]["Lastname1"].ToString() + " " + dtCustomer.Rows[0]["Lastname2"].ToString()).FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("ID: ").SemiBold().FontSize(10);
                                txt.Span(dtCustomer.Rows[0]["Id"].ToString()).FontSize(10);
                                txt.Span("123456").FontSize(10);
                            });

                            col2.Item().Text(txt =>
                            {
                                txt.Span("Dirección: ").SemiBold().FontSize(10);
                                txt.Span("av. Miraflores 123").FontSize(10);
                            });
                        });

                        col1.Item().LineHorizontal(0.5f);

                        col1.Item().Table(tabla => {

                            tabla.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            tabla.Header(header =>
                            {
                                header.Cell().Background("#257272")
                                .Padding(2).Text("Producto").FontColor("#fff");

                                header.Cell().Background("#257272")
                                .Padding(2).Text("Cantidad").FontColor("#fff");

                                header.Cell().Background("#257272")
                                .Padding(2).Text("Precio").FontColor("#fff");

                                header.Cell().Background("#257272")
                                .Padding(2).AlignRight().Text("Total").FontColor("#fff");
                            });

                            for (int i = 0; i < dtSaleItems.Rows.Count; i++)
                            {
                                var quantity = dtSaleItems.Rows[i]["Quantity"].ToString();
                                var price = dtSaleItems.Rows[i]["Price"].ToString();
                                var total = Convert.ToDouble(quantity) * Convert.ToDouble(price);


                                object valor = dtSaleItems.Rows[i].ItemArray[5];
                                var description = ((Product)valor).Description;


                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(description).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text(quantity).FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).Text($" {price}").FontSize(10);

                                tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9")
                                .Padding(2).AlignRight().Text($" {total}").FontSize(10);

                            }
                        });
                        col1.Item().AlignRight().Text("Total: " + totalVenta).FontSize(12).Bold();
                        
                        col1.Spacing(10);
                    });

                    page.Footer().AlignRight().Text(txt =>
                    {
                        txt.Span("Página ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });

                });

            });

            byte[] pdfBytes = document.GeneratePdf();
            MemoryStream ms = new MemoryStream(pdfBytes);

            return ms;
        }

       


    }
}
