using ByteBuy.Core.Domain.Enums;
using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Core.ServiceContracts;

namespace ByteBuy.Core.Services;

public class DocumentService : IDocumentService
{
    private readonly IDocumentRepository _documentRepository;
    private readonly IPdfGenerator<OrderDetailsPdfModel> _pdfGenerator;

    public DocumentService(IDocumentRepository documentRepository, 
        IPdfGenerator<OrderDetailsPdfModel> pdfGenerator)
    {
        _documentRepository = documentRepository;
        _pdfGenerator = pdfGenerator;
    }

    public async Task<byte[]> GenerateOrderDetailsPdf(Guid orderId, CancellationToken ct)
    {
        var pdfData = await _documentRepository.GetOrderDetails(orderId, ct);
        if (pdfData is null)
            return [];

        if(pdfData.OrderStatus == OrderStatus.Returned 
            || pdfData.OrderStatus ==  OrderStatus.Delivered)
        {
            var generated = _pdfGenerator.Generate(pdfData);

            return generated;
        }

        return [];
    }
}
