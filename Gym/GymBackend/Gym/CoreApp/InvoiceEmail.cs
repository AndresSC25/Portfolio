using System.Xml.Serialization;
using DataAccess.CRUD;
using DTOs;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;

public class InvoiceEmailSender
{
    private readonly string _apiKey;

    public InvoiceEmailSender()
    {
        _apiKey = "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";
    }

    public async Task SendInvoiceEmailAsync(Invoice invoice, List<Detail> details, User user)
    {
        Settings.License = LicenseType.Community;

        var pdfContent = GeneratePdf(invoice, details, user);
        var xmlContent = GenerateXml(invoice, details, user);

        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress("siluetaclubfitness@gmail.com", "Silueta Club Fitness");
        var to = new EmailAddress(user.Email);
        var templateId = "d-fb2be50c94204770ad738f67d49c8edc";
        var dynamicTemplateData = new
        {
            subject = $"Silueta Club Fitness - Recibo - {DateTime.Now:dd-MM-yyyy}"
        };
        var msg = MailHelper.CreateSingleTemplateEmail(from, to, templateId, dynamicTemplateData);

        var pdfAttachment = new Attachment
        {
            Content = Convert.ToBase64String(pdfContent),
            Type = "application/pdf",
            Filename = $"Recibo - #{invoice.Id.ToString()} - {user.Name + " " + user.LastName}.pdf",
            Disposition = "attachment"
        };
        msg.AddAttachment(pdfAttachment);

        var xmlAttachment = new Attachment
        {
            Content = Convert.ToBase64String(xmlContent),
            Type = "application/xml",
            Filename = $"Recibo - #{invoice.Id.ToString()} - {user.Name + " " + user.LastName}.xml",
            Disposition = "attachment"
        };
        msg.AddAttachment(xmlAttachment);

        try
        {
            var response = await client.SendEmailAsync(msg);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
        }
    }

    public byte[] GeneratePdf(Invoice invoice, List<Detail> details, User user)
    {
        var pdfDocument = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);

                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        column.Item().Text("Silueta Club Fitness").FontSize(16).Bold();
                        column.Item().Text("Cédula Jurídica: 4-101-747234").FontSize(10);
                        column.Item().Text("Tel: 87654321").FontSize(10);
                        column.Item().Text("Email: siluetaclubfitness@gmail.com").FontSize(10);
                        column.Item().Text("Horario: Lunes a Viernes de 6am a 10pm").FontSize(10);
                    });
                });

                page.Content().Column(column =>
                {
                    column.Item().Height(20);

                    column.Item().AlignRight().Text(text =>
                    {
                        text.Span("Recibo #").Bold();
                        text.Span(invoice.Id.ToString())
                            .FontColor(Colors.Orange.Medium);
                        column.Item().Height(10);
                        text.Span(" Fecha: ").Bold();
                        text.Span($"{DateTime.Now:dd/MM/yyyy}")
                            .FontColor(Colors.Orange.Medium);
                    });

                    column.Item().Height(10);

                    column.Item().Element(content => { BuildClientDetailsTable(user, content); });

                    column.Item().PaddingVertical(10);

                    column.Item().Element(content => { BuildTable(invoice, details, content); });
                });

                page.Footer().AlignCenter().Text("Silueta Club Fitness - 2024");
            });
        });

        using (var memoryStream = new MemoryStream())
        {
            pdfDocument.GeneratePdf(memoryStream);
            return memoryStream.ToArray();
        }
    }

    private void BuildClientDetailsTable(User user, IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(4);
                columns.RelativeColumn(8);
            });

            table.Header(header =>
            {
                header.Cell().ColumnSpan(2).Element(HeaderCellStyle).Text("Detalles del Cliente");
            });

            table.Cell().Element(LabelCellStyle).Text("Nombre:");
            table.Cell().Element(DataCellStyle).Text(user.Name + " " + user.LastName);

            table.Cell().Element(LabelCellStyle).Text("Número de Teléfono:");
            table.Cell().Element(DataCellStyle).Text(user.Phone);

            table.Cell().Element(LabelCellStyle).Text("Correo Electrónico:");
            table.Cell().Element(DataCellStyle).Text(user.Email);
        });
    }

    private void BuildTable(Invoice invoice, List<Detail> details, IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(4);
                columns.RelativeColumn(2);
            });

            table.Header(header =>
            {
                header.Cell().Element(HeaderCellStyle).Text("Detalle");
                header.Cell().Element(HeaderCellStyle).AlignRight().Text("Precio");
            });

            var alternateRow = false;
            foreach (var detail in details)
            {
                if (detail.UserMembershipId != 2)
                {
                    var mCrud = new MembershipCrudFactory();
                    var membership = mCrud.RetrieveById<Membership>(invoice.MembershipID ?? 0);
                    table.Cell().Element(c => DetailCellStyle(c, alternateRow))
                        .Text("Tipo de Membresia: " + membership.Type);
                }
                else
                {
                    var ptCrud = new PersonalTrainingCrudFactory();
                    var pTraining = ptCrud.RetrieveById<PersonalTraining>(detail.PersonalTrainingId ?? 0);
                    table.Cell().Element(c => DetailCellStyle(c, alternateRow))
                        .Text("Entrenamiento Personal el dia: " + pTraining.ProgrammedDate);
                }

                table.Cell().Element(c => DetailCellStyle(c, alternateRow)).AlignRight().Text($"₡{detail.Price:N}");
                ;
                alternateRow = !alternateRow;
            }

            table.Cell().Element(TotalCellStyle).Text("Subtotal");
            table.Cell().Element(TotalCellStyle).AlignRight().Text($"₡{invoice.Amount:N}");

            table.Cell().Element(TotalCellStyle).Text("Descuento");
            table.Cell().Element(TotalCellStyle).AlignRight()
                .Text($"₡{Math.Abs(invoice.Amount - invoice.AmountAfterDiscount):N}");

            table.Cell().Element(TotalCellStyle).Text("Total");
            table.Cell().Element(TotalCellStyle).AlignRight().Text($"₡{invoice.AmountAfterDiscount:N}");

            table.Cell().Element(TotalCellStyle).Text("Metodo de Pago");
            table.Cell().Element(TotalCellStyle).AlignRight().Text(invoice.PaymentMethod);
        });
    }

    private IContainer HeaderCellStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .BorderColor(Colors.Grey.Lighten2)
            .Background(Colors.Orange.Medium)
            .Padding(5)
            .AlignMiddle()
            .AlignCenter()
            .DefaultTextStyle(x => x.FontSize(14).FontColor(Colors.White).Bold());
    }

    private IContainer LabelCellStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .BorderColor(Colors.Grey.Lighten2)
            .Background(Colors.Grey.Lighten4)
            .Padding(5)
            .AlignMiddle()
            .DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Grey.Darken3).Bold());
    }

    private IContainer DataCellStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .BorderColor(Colors.Grey.Lighten2)
            .Background(Colors.White)
            .Padding(5)
            .AlignMiddle()
            .DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Grey.Darken3));
    }

    private IContainer DetailCellStyle(IContainer container, bool alternate)
    {
        return container
            .Border(0.5f)
            .BorderColor(Colors.Grey.Lighten2)
            .Background(alternate ? Colors.Grey.Lighten4 : Colors.White)
            .Padding(5)
            .AlignMiddle()
            .DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Grey.Darken3));
    }

    private IContainer TotalCellStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .BorderColor(Colors.Grey.Lighten2)
            .Background(Colors.Orange.Lighten5)
            .Padding(5)
            .AlignMiddle()
            .DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Orange.Darken2).Bold());
    }

    private IContainer CellStyle(IContainer container)
    {
        return container
            .Border(0.5f)
            .Padding(5)
            .Background(Colors.White)
            .AlignMiddle()
            .DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));
    }

    private byte[] GenerateXml(Invoice invoice, List<Detail> details, User user)
    {
        using (var memoryStream = new MemoryStream())
        {
            var fullInvoiceData = new FullInvoiceData
            {
                Invoice = invoice,
                Details = details,
                User = user
            };

            var serializer = new XmlSerializer(typeof(FullInvoiceData));
            serializer.Serialize(memoryStream, fullInvoiceData);

            return memoryStream.ToArray();
        }
    }

    [Serializable]
    public class FullInvoiceData
    {
        public Invoice Invoice { get; set; }
        public List<Detail> Details { get; set; }
        public User User { get; set; }
    }
}