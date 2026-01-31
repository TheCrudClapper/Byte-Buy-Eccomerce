using ByteBuy.API.Extensions;
using ByteBuy.Core.ResultTypes;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace ByteBuy.API.Controllers.Base;

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
        var statusCode = MapToStatusCode(result.Error);

        return result.IsFailure
            ? Problem(
                statusCode: (int)statusCode,
                title: result.Error.Code,
                detail: result.Error.Description)
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
        var statusCode = MapToStatusCode(result.Error);

        return result.IsFailure
            ? Problem(
                statusCode: (int)statusCode,
                title: result.Error.Code,
                detail: result.Error.Description)
            : NoContent();
    }

    private static HttpStatusCode MapToStatusCode(Error error)
        => error.Type switch
        {
            ErrorType.Validation => HttpStatusCode.BadRequest,
            ErrorType.NotFound => HttpStatusCode.NotFound,
            ErrorType.Conflict => HttpStatusCode.Conflict,
            ErrorType.Unauthorized => HttpStatusCode.Unauthorized,
            ErrorType.Forbidden => HttpStatusCode.Forbidden,
            _ => HttpStatusCode.InternalServerError,
        };

}

