using ByteBuy.Core.Domain.RepositoryContracts;
using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Core.ResultTypes;
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

    public async Task<Result<byte[]>> GenerateOrderDetailsPdfAsync(Guid orderId, CancellationToken ct)
    {
        var pdfData = await _documentRepository.GetOrderDetails(orderId, ct);
        if (pdfData is null)
            return Result.Failure<byte[]>(DocumentErrors.NotFound);

        var generated = _pdfGenerator.Generate(pdfData);
        return generated;
    }
}
