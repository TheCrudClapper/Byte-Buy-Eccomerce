using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using ByteBuy.API.Extensions;
namespace ByteBuy.API.Controllers;

/// <summary>
/// Helper Class to reduce boilerplate in controllers.
/// </summary>
public class BaseApiController : ControllerBase
{
    /// <summary>
    /// Gets Logged User Id from Authentication Token
    /// </summary>
    protected Guid CurrentUserId => this.GetLoggedUserId();

    /// <summary>
    /// Handles controller action that returns data back to client with HTTP Code 200
    /// </summary>
    /// <typeparam name="T">Type that will be returned back to client</typeparam>
    /// <param name="result">Result object as result of service call</param>
    /// <returns></returns>
    [NonAction]
    public ActionResult HandleResult<T>(Result<T> result)
    {
        return result.IsFailure
            ? Problem(statusCode: result.Error.ErrorCode, detail: result.Error.Description)
            : Ok(result.Value);
    }


    /// <summary>
    /// Handles controller action that doesn't return data back to client
    /// Operation Successfull - 204 No Content
    /// Operation Faild - Problem Details
    /// </summary>
    /// <param name="result">Result object as result of service call</param>
    /// <returns></returns>
    [NonAction]
    public ActionResult HandleResult(Result result)
    {
        return result.IsFailure
            ? Problem(statusCode: result.Error.ErrorCode, detail: result.Error.Description)
            : NoContent();
    }
}

