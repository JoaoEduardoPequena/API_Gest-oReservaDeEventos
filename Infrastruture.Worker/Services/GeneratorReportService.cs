using Infrastruture.Worker.DTO;
using Infrastruture.Worker.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastruture.Worker.Services
{
    public class GeneratorReportService : IGeneratorReportService
    {
        public async Task<byte[]> GenerateReservaPdfAsync(EmailDTO dto)
        {
            using var stream = new MemoryStream();
            var currentDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(40);
                    page.Size(PageSizes.A4);

                    // Cabeçalho
                    page.Header().Row(row =>
                    {
                        row.RelativeColumn().Text(dto.EmailSubject)
                            .FontSize(22)
                            .Bold()
                            .FontColor(Colors.Blue.Medium);

                        row.ConstantColumn(80).Height(40).AlignRight().Image(Placeholders.LoremIpsum());
                    });

                    // Corpo
                    page.Content().PaddingVertical(20).Column(col =>
                    {
                        col.Spacing(15);

                        col.Item().Text($"Prezado(a), {dto.User}!").FontSize(16).Bold();
                        col.Item().Text("Segue abaixo o resumo da sua reserva:")
                            .FontSize(12).FontColor(Colors.Grey.Darken2);

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(140);
                                columns.RelativeColumn();
                            });

                            void AddRow(string label, string value)
                            {
                                table.Cell().Element(CellStyle).Text(label).Bold();
                                table.Cell().Element(CellStyle).Text(value ?? "-");
                            }

                            AddRow("Nome:", dto.User);
                            AddRow("E-mail:", dto.To);
                            AddRow("Data de Emissão:", currentDate);
                            AddRow("Mensagem:", dto.EmailText);

                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .BorderBottom(0.5f)
                                    .BorderColor(Colors.Grey.Lighten2)
                                    .PaddingVertical(6);
                            }
                        });

                        col.Item().PaddingTop(20)
                            .Text("Obrigado por usar o nosso sistema de reservas!")
                            .FontColor(Colors.Blue.Medium)
                            .FontSize(12)
                            .Italic()
                            .AlignCenter();
                    });

                    // Rodapé
                    page.Footer().AlignCenter().Text(txt =>
                    {
                        txt.Span("© ").FontColor(Colors.Grey.Darken1);
                        txt.Span($"{DateTime.Now.Year} Sistema de Reservas")
                            .SemiBold().FontColor(Colors.Blue.Darken1);
                    });
                });
            });

            // Gerar PDF
            document.GeneratePdf(stream);
            stream.Position = 0;

            return await Task.FromResult(stream.ToArray());
        }
    }
}
