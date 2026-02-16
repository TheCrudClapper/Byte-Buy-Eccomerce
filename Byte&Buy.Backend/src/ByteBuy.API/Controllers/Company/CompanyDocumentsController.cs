using ByteBuy.API.Controllers.Base;
using ByteBuy.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Company;

[Route("api/company/documents")]
[ApiController]
public class CompanyDocumentsController : BaseApiController
{
    private readonly IDocumentService _documentService;
    public CompanyDocumentsController(IDocumentService documentService)
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
