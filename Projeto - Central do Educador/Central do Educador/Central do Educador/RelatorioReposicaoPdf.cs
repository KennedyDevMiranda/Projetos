using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class RelatorioReposicaoPdf : IDocument
{
    private readonly List<RegistroTaxa> _lista;
    private readonly string _titulo;
    private readonly string _subtitulo;
    private readonly decimal _valorTaxa;
    private readonly decimal _percComissao;

    public RelatorioReposicaoPdf(
        List<RegistroTaxa> lista,
        string titulo,
        string subtitulo,
        decimal valorTaxa,
        decimal percComissao)
    {
        _lista = lista ?? new();
        _titulo = titulo;
        _subtitulo = subtitulo;
        _valorTaxa = valorTaxa;
        _percComissao = percComissao;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(25);
            page.Size(PageSizes.A4);
            page.DefaultTextStyle(x => x.FontSize(10));

            page.Header().Element(ComposeHeader);

            page.Content().Element(ComposeContent);

            page.Footer().Element(ComposeFooter);
        });
    }

    private void ComposeHeader(IContainer container)
    {
        container.Column(col =>
        {
            col.Item().Text(_titulo).FontSize(16).SemiBold();
            col.Item().Text(_subtitulo).FontSize(10).FontColor(Colors.Grey.Darken2);

            col.Item().PaddingTop(8).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.PaddingTop(10).Column(col =>
        {
            col.Item().Element(ComposeTable);

            col.Item().PaddingTop(12).Element(ComposeTotaisBox);
        });
    }

    private void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                //columns.ConstantColumn(40);  // Id
                columns.RelativeColumn(3);   // Aluno
                columns.ConstantColumn(75);  // Falta
                columns.ConstantColumn(75);  // Reposição
                columns.ConstantColumn(55);  // Qtd
                columns.ConstantColumn(70);  // Lançado
                columns.RelativeColumn(2);   // Obs
            });

            // Cabeçalho
            table.Header(header =>
            {
                //header.Cell().Element(HeaderCellStyle).Text("ID");
                header.Cell().Element(HeaderCellStyle).Text("Aluno");
                header.Cell().Element(HeaderCellStyle).Text("Falta");
                header.Cell().Element(HeaderCellStyle).Text("Reposição");
                header.Cell().Element(HeaderCellStyle).AlignCenter().Text("Qtd");
                header.Cell().Element(HeaderCellStyle).AlignCenter().Text("Lançado");
                header.Cell().Element(HeaderCellStyle).Text("Observação");
            });

            // Linhas
            foreach (var item in _lista.OrderByDescending(x => x.Id))
            {
                //table.Cell().Element(BodyCellStyle).Text(item.Id.ToString());
                table.Cell().Element(BodyCellStyle).Text(item.Aluno);

                table.Cell().Element(BodyCellStyle).Text(item.DataFalta.ToString("dd/MM/yyyy"));

                table.Cell().Element(BodyCellStyle).Text(
                    item.DataReposicao.HasValue ? item.DataReposicao.Value.ToString("dd/MM/yyyy") : "Não agendado");

                table.Cell().Element(BodyCellStyle).AlignCenter().Text(item.Quantidade.ToString());

                table.Cell().Element(BodyCellStyle).AlignCenter().Text(item.Lancado ? "Sim" : "Não");

                table.Cell().Element(BodyCellStyle).Text(string.IsNullOrWhiteSpace(item.Observacao) ? "-" : item.Observacao);
            }

            // Estilos
            static IContainer HeaderCellStyle(IContainer c) =>
                c.DefaultTextStyle(x => x.SemiBold().FontColor(Colors.White))
                 .Background(Colors.Blue.Darken2)
                 .PaddingVertical(6).PaddingHorizontal(6);

            static IContainer BodyCellStyle(IContainer c) =>
                c.BorderBottom(1).BorderColor(Colors.Grey.Lighten2)
                 .PaddingVertical(5).PaddingHorizontal(6);
        });
    }

    private void ComposeTotaisBox(IContainer container)
    {
        var qtdTotal = _lista.Sum(x => Math.Max(0, x.Quantidade));

        var total = Math.Round(qtdTotal * _valorTaxa, 2, MidpointRounding.AwayFromZero);
        var comissao = Math.Round(total * (_percComissao / 100m), 2, MidpointRounding.AwayFromZero);

        container
            .Border(1).BorderColor(Colors.Grey.Lighten2)
            .Background(Colors.Grey.Lighten4)
            .Padding(10)
            .Column(col =>
            {
                // Linha 1: contagens
                col.Item().Row(row =>
                {
                    row.RelativeItem().Text($"Total registros: {_lista.Count}");
                    row.RelativeItem().Text($"Qtd. faltas: {qtdTotal}");
                });

                col.Item().PaddingTop(6).LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                // Linha 2: valores (bem visíveis)
                col.Item().PaddingTop(6).Row(row =>
                {
                    row.RelativeItem().Text($"Valor da taxa: R$ {_valorTaxa:N2}").SemiBold();
                    row.RelativeItem().AlignRight().Text($"Comissão: {_percComissao:N2}%").SemiBold();
                });

                // Linha 3: total e comissão em dinheiro
                col.Item().PaddingTop(4).Row(row =>
                {
                    row.RelativeItem().Text($"Total: R$ {total:N2}").FontSize(12).SemiBold();
                    row.RelativeItem().AlignRight().Text($"Comissão (R$): {comissao:N2}").FontSize(12).SemiBold();
                });
            });
    }

    private void ComposeFooter(IContainer container)
    {
        container.Column(col =>
        {
            col.Item()
                .LineHorizontal(1)
                .LineColor(Colors.Grey.Lighten2);

            col.Item()
                .PaddingTop(6)
                .Row(row =>
                {
                    row.RelativeItem()
                        .Text($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}")
                        .FontColor(Colors.Grey.Darken2);

                    row.RelativeItem()
                        .AlignRight()
                        .Text(t =>
                        {
                            t.Span("Página ");
                            t.CurrentPageNumber();
                            t.Span(" de ");
                            t.TotalPages();
                        });
                });
        });
    }
}