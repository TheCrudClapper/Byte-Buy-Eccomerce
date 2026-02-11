using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.DTO.Internal.DocumentModels;
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

    [HttpGet("order-summary")]
    public async Task<ActionResult<FileContentResult>> DownloadOrderDetailsPdf(Guid orderId, CancellationToken ct)
    {
        var pdfBytes = await _documentService.GenerateOrderDetailsPdf(orderId, ct);

        return File(pdfBytes, "application/pdf", $"order-details-{orderId}.pdf");
    }
}
