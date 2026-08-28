using LibraryManagement.Constants;
using LibraryManagement.Results;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers {
    public class BaseApiController:ControllerBase {

        protected ActionResult<T> ToActionResult<T>(Result<T> result)
            => result.IsSuccess ? Ok(result.Value) : MapErrorsToResponse(result.Errors);

        protected ActionResult ToActionResult(Result result)
            => result.IsSuccess ? NoContent() : MapErrorsToResponse(result.Errors);

        protected ActionResult MapErrorsToResponse(Error[] errors) {
            if (errors.Length == 0 || errors is null) return Problem();

            var e = errors[0];
            return e.Code switch {
                ErrorCodes.NotFound => NotFound(e.Description),
                ErrorCodes.Validation => BadRequest(e.Description),
                ErrorCodes.BadRequest => BadRequest(e.Description),
                ErrorCodes.Conflict => Conflict(e.Description),

            };
        }
    }
}
