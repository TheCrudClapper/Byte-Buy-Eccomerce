using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using ByteBuy.API.Extensions;
namespace ByteBuy.API.Controllers;
public class BaseApiController : ControllerBase
{
    protected Guid CurrentUserId => this.GetLoggedUserId();

    [NonAction]
    public ActionResult HandleResult<T>(Result<T> result)
    {
        return result.IsFailure
            ? Problem(statusCode: result.Error.ErrorCode, detail: result.Error.Description)
            : Ok(result.Value);
    }

    [NonAction]
    public ActionResult HandleResult(Result result)
    {
        return result.IsFailure
            ? Problem(statusCode: result.Error.ErrorCode, detail: result.Error.Description)
            : NoContent();
    }
}

