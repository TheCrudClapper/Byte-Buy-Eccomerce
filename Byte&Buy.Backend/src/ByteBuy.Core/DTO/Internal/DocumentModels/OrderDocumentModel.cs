namespace ByteBuy.Core.DTO.Internal.DocumentModels;

public sealed record OrderDocumentModel
{
    public Guid OrderId { get; init; }
    public DateTime DateCreated { get; init; }
    public decimal Total { get; init; }
    public decimal Tax { get; init; }
    public string TaxCurrency { get; init; } = null!;
    public string TotalCurrency { get; init; } = null!;
    public decimal LinesTotal { get; init; }
    public string LinesTotalCurrency { get; init; } = null!;
    public IReadOnlyCollection<OrderLineDocumentModel> Lines { get; init; } = [];
}
