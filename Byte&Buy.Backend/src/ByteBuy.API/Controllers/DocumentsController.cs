using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Internal.DocumentModels;
using ByteBuy.Core.ResultTypes;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/documents")]
[ApiController]
public class DocumentsController : BaseApiController
{
    private readonly IDocumentService _documentService;
    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet("order-details/{orderId:guid}")]
    public async Task<ActionResult> DownloadOrderDetailsPdf(Guid orderId, CancellationToken ct)
    {
        var pdfBytesResult = await _documentService.GenerateOrderDetailsPdf(orderId, ct);

        return pdfBytesResult.IsFailure
            ? Problem(
                statusCode: 404,
                title: pdfBytesResult.Error.Code,
                detail: pdfBytesResult.Error.Description)
            : File(pdfBytesResult.Value, "application/pdf", $"order-details-{orderId}.pdf");
    }
}
