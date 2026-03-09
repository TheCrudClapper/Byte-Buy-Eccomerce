using ByteBuy.Core.Domain.Orders.Entities;
using ByteBuy.Core.Domain.Orders.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Core.DTO.Internal.Order.Enum;
using ByteBuy.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ByteBuy.Infrastructure.Repositories;

public class DocumentRepository : IDocumentRepository
{
    private readonly ApplicationDbContext _context;
    public DocumentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    private async Task<CompanyDocumentModel?> GetCompanyDataForDocument(CancellationToken ct)
    {
        return await _context.Company.Select(c => new CompanyDocumentModel()
        {
            CompanyName = c.CompanyName,
            Email = c.Email,
            Phone = c.Phone,
            TIN = c.TIN,
            CompanyAddress = new AddressDocumentModel()
            {
                City = c.CompanyAddress.City,
                Country = c.CompanyAddress.Country,
                FlatNumber = c.CompanyAddress.FlatNumber,
                HouseNumber = c.CompanyAddress.HouseNumber,
                PostalCode = c.CompanyAddress.PostalCode,
                PostalCity = c.CompanyAddress.PostalCity,
                Street = c.CompanyAddress.Street
            }
        }).SingleAsync(ct);
    }

    public async Task<OrderDetailsPdfModel?> GetOrderDetails(Guid orderId, CancellationToken ct)
    {
        var companyData = await GetCompanyDataForDocument(ct);

        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.Id == orderId
                && o.DateDelivered != null
                && (o.Status == OrderStatus.Delivered || o.Status == OrderStatus.Returned))
            .Select(o => new OrderDetailsPdfModel()
            {
                OrderStatus = o.Status,
                OrderData = new OrderDocumentModel()
                {
                    Total = o.Total.Amount,
                    Tax = o.Total.Amount * 0.23m,
                    TaxCurrency = o.Total.Currency,
                    TotalCurrency = o.Total.Currency,
                    OrderId = o.Id,
                    LinesTotal = o.LinesTotal.Amount,
                    LinesTotalCurrency = o.LinesTotal.Currency,
                    DateCreated = o.DateCreated,
                    Lines = o.Lines.Select(l => new OrderLineDocumentModel()
                    {
                        ItemTitle = l.ItemName,
                        Type = l is RentOrderLine ? OrderLineType.Rent : OrderLineType.Sale,

                        PricePerDay = l is RentOrderLine ? ((RentOrderLine)l).PricePerDay.Amount : null,
                        PricePerDayCurrency = l is RentOrderLine ? ((RentOrderLine)l).PricePerDay.Currency : null,

                        Quantity = l.Quantity,
                        RentalDays = l is RentOrderLine ? ((RentOrderLine)l).RentalDays : null,
                        PricePerItem = l is SaleOrderLine ? ((SaleOrderLine)l).PricePerItem.Amount : null,
                        PricePerItemCurrency = l is SaleOrderLine ? ((SaleOrderLine)l).PricePerItem.Currency : null,
                        Total = l.TotalPrice.Amount,
                        TotalCurrency = l.TotalPrice.Currency
                    }).ToList(),
                },
                PaymentData = new PaymentDocumentModel()
                {
                    DateCreated = o.Payment.DateCreated,
                    PaymentId = o.Payment.PaymentId,
                    Method = o.Payment.Payment.Method.ToString(),
                    Total = o.Payment.Amount.Amount,
                    TotalCurrency = o.Payment.Amount.Currency
                },
                DeliveryData = new DeliveryDocumentModel()
                {
                    CarrierCode = o.Delivery.CarrierCode,
                    DeliveredDate = o.DateDelivered.GetValueOrDefault(),
                    DeliveryName = o.Delivery.DeliveryName,
                    Total = o.Delivery.Price.Amount,
                    TotalCurrency = o.Delivery.Price.Currency
                },
                CustomerData = new CustomerDocumentModel()
                {
                    Email = o.BuyerSnapshot.Email,
                    CustomersFullName = o.BuyerSnapshot.FullName,
                    Phone = o.BuyerSnapshot.PhoneNumber!,
                    CustomerAddress = new AddressDocumentModel()
                    {
                        City = o.BuyerSnapshot.Address.City,
                        FlatNumber = o.BuyerSnapshot.Address.FlatNumber,
                        HouseNumber = o.BuyerSnapshot.Address.HouseNumber,
                        Country = o.BuyerSnapshot.Address.Country,
                        PostalCity = o.BuyerSnapshot.Address.PostalCity,
                        PostalCode = o.BuyerSnapshot.Address.PostalCode,
                        Street = o.BuyerSnapshot.Address.Street,
                    }
                },
                CompanyData = companyData!,
            }).FirstOrDefaultAsync(ct);
    }
}
