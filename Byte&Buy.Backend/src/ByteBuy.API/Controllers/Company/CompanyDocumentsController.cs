namespace ByteBuy.API.Controllers.Company;

[Resource("company-documents")]
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
    [HasPermission("{resource}:read:order-details")]
    public async Task<ActionResult> DownloadOrderDetailsPdfAsync(Guid orderId, CancellationToken ct)
    {
        var pdfBytesResult = await _documentService.GenerateOrderDetailsPdfAsync(orderId, ct);

        return pdfBytesResult.IsFailure
            ? Problem(
                statusCode: 404,
                title: pdfBytesResult.Error.Code,
                detail: pdfBytesResult.Error.Description)
            : File(pdfBytesResult.Value, "application/pdf", $"order-details-{orderId}.pdf");
    }
}
