using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Core.DTO.Internal.Order.Enum;
using ByteBuy.Core.ServiceContracts;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ByteBuy.Infrastructure.Services;

public class QuestOrderDetailsPdfGenerator : IPdfGenerator<OrderDetailsPdfModel>
{
    private static decimal Round(decimal value)
    {
        return decimal.Round(value, 2, MidpointRounding.AwayFromZero);
    }

    public byte[] Generate(OrderDetailsPdfModel orderDetails)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(10));
                page.PageColor(Colors.White);

                page.Content().Column(col =>
                {

                    col.Spacing(20);

                    // Header, company data
                    col.Item().Row(row =>
                    {
                        row.RelativeItem().Column(company =>
                        {
                            company.Item().Text(orderDetails.CompanyData.CompanyName)
                                .Bold().FontSize(14);

                            company.Item().Text($"{orderDetails.CompanyData.CompanyAddress.Street} {orderDetails.CompanyData.CompanyAddress.HouseNumber}/{orderDetails.CompanyData.CompanyAddress.FlatNumber}");
                            company.Item().Text($"{orderDetails.CompanyData.CompanyAddress.PostalCode} {orderDetails.CompanyData.CompanyAddress.PostalCity}, {orderDetails.CompanyData.CompanyAddress.Country}");
                            company.Item().Text($"TIN: {orderDetails.CompanyData.TIN}");
                            company.Item().Text(orderDetails.CompanyData.Email);
                            company.Item().Text($"+ 48 {orderDetails.CompanyData.Phone}");
                        });

                        row.ConstantItem(200).AlignRight().Column(meta =>
                        {
                            meta.Item().Text("Created").Bold();
                            meta.Item().Text(orderDetails.OrderData.DateCreated.ToString("dd.MM.yyyy HH:mm"));
                        });
                    });

                    col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                    // Title
                    col.Item().Text($"Order details: {orderDetails.OrderData.OrderId}")
                        .Bold()
                        .FontSize(14);

                    // 3 columns for recipient. delivery, payment
                    col.Item().Row(row =>
                    {
                        row.Spacing(20);

                        // Recipient Data
                        row.RelativeItem().Column(rec =>
                        {
                            rec.Item().Text("Recipient").Bold();
                            rec.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                            rec.Item().Text(orderDetails.CustomerData.CustomersFullName);
                            rec.Item().Text(orderDetails.CustomerData.Email);
                            rec.Item().Text($"+ 48 { orderDetails.CustomerData.Phone}");
                            rec.Item().Text($"{orderDetails.CustomerData.CustomerAddress.City} {orderDetails.CustomerData.CustomerAddress.Street} {orderDetails.CustomerData.CustomerAddress.HouseNumber}/{orderDetails.CustomerData.CustomerAddress.FlatNumber}");
                            rec.Item().Text($"{orderDetails.CustomerData.CustomerAddress.PostalCode} {orderDetails.CustomerData.CustomerAddress.PostalCity}");
                            rec.Item().Text(orderDetails.CustomerData.CustomerAddress.Country);
                        });

                        // Delivery
                        row.RelativeItem().Column(del =>
                        {
                            del.Item().Text("Delivery Method").Bold();
                            del.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                            del.Item().Text(orderDetails.DeliveryData.DeliveryName);
                            del.Item().Text($"Carrier: {orderDetails.DeliveryData.CarrierCode}");
                            del.Item().Text($"Cost: {Round(orderDetails.DeliveryData.Total)} {orderDetails.DeliveryData.TotalCurrency}");
                            del.Item().Text($"Delivered: {orderDetails.DeliveryData.DeliveredDate:dd.MM.yyyy}");
                        });

                        // Payment
                        row.RelativeItem().Column(pay =>
                        {
                            pay.Item().Text("Payment").Bold();
                            pay.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                            pay.Item().Text($"Payment ID: {orderDetails.PaymentData.PaymentId}");
                            pay.Item().Text($"Date: {orderDetails.PaymentData.DateCreated:dd.MM.yyyy}");
                            pay.Item().Text($"Method: {orderDetails.PaymentData.Method}");
                            pay.Item().Text($"Total: {Round(orderDetails.PaymentData.Total)} {orderDetails.PaymentData.TotalCurrency}");
                        });
                    });

                    // Table for items
                    col.Item().PaddingTop(20).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(4); // Item
                            columns.RelativeColumn(1); // Qty
                            columns.RelativeColumn(1); // Days
                            columns.RelativeColumn(2); // Price
                            columns.RelativeColumn(2); // Amount
                        });


                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Item").Bold();
                            header.Cell().Element(CellStyle).AlignCenter().Text("Qty").Bold();
                            header.Cell().Element(CellStyle).AlignCenter().Text("Days").Bold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Price").Bold();
                            header.Cell().Element(CellStyle).AlignRight().Text("Amount").Bold();
                        });

                        // Lines rows
                        foreach (var line in orderDetails.OrderData.Lines)
                        {
                            table.Cell().Element(CellStyle)
                                .Text(line.ItemTitle);

                            table.Cell().Element(CellStyle)
                                .AlignCenter()
                                .Text(line.Quantity.ToString());

                            table.Cell().Element(CellStyle)
                                .AlignCenter()
                                .Text(line.RentalDays is null
                                    ? "-"
                                    : line.RentalDays.Value.ToString());

                            if (line.Type == OrderLineType.Rent)
                            {
                                table.Cell().Element(CellStyle)
                                .AlignRight()
                                .Text($"{Round(line.PricePerDay ?? -1m)} {line.PricePerDayCurrency}");
                            }
                            else
                            {
                                table.Cell().Element(CellStyle)
                                .AlignRight()
                                .Text($"{Round(line.PricePerItem ?? -1m)} {line.PricePerItemCurrency}");
                            }

                            table.Cell().Element(CellStyle)
                                .AlignRight()
                                .Text($"{Round(line.Total)} {line.TotalCurrency}");
                        }
                    });

                    // Summary
                    col.Item().AlignRight().PaddingTop(15).Column(sum =>
                    {

                        sum.Item().Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text("Subtotal");
                            row.ConstantItem(100).AlignRight().Text($"{Round(orderDetails.OrderData.LinesTotal)} {orderDetails.OrderData.LinesTotalCurrency}");
                        });

                        sum.Item().Padding(2);
                        sum.Item().Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text("Tax");
                            row.ConstantItem(100).AlignRight().Text($"{Round(orderDetails.OrderData.Tax)} {orderDetails.OrderData.TaxCurrency}");
                        });

                        sum.Item().Padding(2);
                        sum.Item().Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text("Delivery");
                            row.ConstantItem(100).AlignRight().Text($"{Round(orderDetails.DeliveryData.Total)} {orderDetails.DeliveryData.TotalCurrency}");
                        });


                        sum.Item().Padding(10);
                        sum.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);
                        sum.Item().Row(row =>
                        {
                            row.RelativeItem().AlignLeft().Text("Total Due").Bold();
                            row.ConstantItem(100).AlignRight().Text($"{Round(orderDetails.OrderData.Total)} {orderDetails.OrderData.TotalCurrency}").Bold();
                        });

                    });

                    page.Footer().Column(col =>
                    {
                        col.Item().AlignLeft().Text("Disclaimer").Bold();
                        col.Item().AlignLeft().Text("This document is generated automatically.");
                    });
                });
            });


            static IContainer CellStyle(IContainer container)
                => container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);
        }).GeneratePdf();
    }
}
