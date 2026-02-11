using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers;

[Route("api/documents/order-summary")]
[ApiController]
public class DocumentsController : BaseApiController
{
    private readonly IDocumentService _documentService;
    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    public async Task<ActionResult<FileContentResult>> DownloadOrderDetailsPdf(Guid orderId)
    {
        var pdfBytes = await _documentService.GenerateOrderDetailsPdf(orderId);

        return File(pdfBytes, "application/pdf", $"order-details-{orderId}.pdf");
    }
}
