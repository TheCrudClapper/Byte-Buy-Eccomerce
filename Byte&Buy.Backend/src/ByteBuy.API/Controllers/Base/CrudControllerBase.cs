using ByteBuy.Core.DTO;
using ByteBuy.Core.ServiceContracts.Base;
using Microsoft.AspNetCore.Mvc;

namespace ByteBuy.API.Controllers.Base;

/// <summary>
/// Base crud controller that provides standarized action methods for crud,
/// works well with IBaseCrudService
/// </summary>
/// <typeparam name="TId"></typeparam>
/// <typeparam name="TAddRequest"></typeparam>
/// <typeparam name="TUpdateRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
[Route("api/[controller]")]
[ApiController]
public class CrudControllerBase<TId, TAddRequest, TUpdateRequest, TResponse> : BaseApiController
{
    private readonly IBaseCrudService<TId, TAddRequest, TUpdateRequest, TResponse> _service;

    public CrudControllerBase(IBaseCrudService<TId, TAddRequest, TUpdateRequest, TResponse> service)
        => _service = service;

    [HttpPost]
    //[HasPermission("{resource}.create")]
    public virtual async Task<ActionResult<CreatedResponse>> PostAsync(TAddRequest request)
        => HandleResult(await _service.AddAsync(request));

    [HttpPut("{id}")]
    public virtual async Task<ActionResult<UpdatedResponse>> PutAsync(TId id, TUpdateRequest request)
        => HandleResult(await _service.UpdateAsync(id, request));

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteAsync(TId id)
        => HandleResult(await _service.DeleteAsync(id));

    [HttpGet("{id}")]
    //[HasPermission("{resource}:read")]
    public virtual async Task<ActionResult<TResponse>> GetByIdAsync(TId id, CancellationToken cancellationToken)
        => HandleResult(await _service.GetByIdAsync(id, cancellationToken));

}
