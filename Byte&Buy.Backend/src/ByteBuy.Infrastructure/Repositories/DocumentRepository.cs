using ByteBuy.Core.Domain.Entities;
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

    private async Task<CompanyData?> GetCompanyDataForDocument(CancellationToken ct)
    {
        return await _context.Company.Select(c => new CompanyData()
        {
            CompanyName = c.CompanyName,
            Email = c.Email,
            Phone = c.Phone,
            TIN = c.TIN,
            CompanyAddress = new AddressModel()
            {
                City = c.CompanyAddress.City,
                Country = c.CompanyAddress.Country,
                FlatNumber = c.CompanyAddress.FlatNumber,
                HouseNumber = c.CompanyAddress.HouseNumber,
                PostalCode = c.CompanyAddress.PostalCode,
                PostalCity = c.CompanyAddress.PostalCity,
                Street = c.CompanyAddress.Street
            }
        }).SingleOrDefaultAsync(ct);
    }

    public async Task<OrderDetailsPdfModel?> GetOrderDetails(Guid orderId, CancellationToken ct)
    {
        var companyData = await GetCompanyDataForDocument(ct);

        return await _context.Orders
            .AsNoTracking()
            .Where(o => o.Id == orderId)
            .Select(o => new OrderDetailsPdfModel()
            {
                OrderData = new OrderData()
                {
                    OrderId = o.Id,
                    DateCreated = o.DateCreated,
                    Lines = o.Lines.Select(l => new OrderLineData()
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
                PaymentData = new PaymentData()
                {
                    DateCreated = o.Payment.DateCreated,
                    PaymentId = o.Payment.PaymentId,
                    Total = o.Payment.Amount.Amount,
                    TotalCurrency = o.Payment.Amount.Currency
                },
                DeliveryData = new DeliveryData()
                {
                    CarrierCode = o.Delivery.CarrierCode,
                    DeliveredDate = o.DateDelivered!.Value,
                    DeliveryName = o.Delivery.DeliveryName,
                    Total = o.Delivery.Price.Amount,
                    TotalCurrency = o.Delivery.Price.Currency
                },
                CustomerData = new CustomerData()
                {
                    Email = o.BuyerSnapshot.Email,
                    CustomersFullName = o.BuyerSnapshot.FullName,
                    Phone = o.BuyerSnapshot.PhoneNumber!,
                    CustomerAddress = new AddressModel()
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
