using BreweryAPI.BLL.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BreweryAPI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult FromErrorResult(this ControllerBase controller, ServiceResult result)
        {
            return HandleErrorResult(controller, result.ErrorType, result.Message);
        }

        public static IActionResult FromErrorResult<T>(this ControllerBase controller, ServiceResult<T> result) where T : class
        {
            return HandleErrorResult(controller, result.ErrorType, result.Message);
        }

        private static IActionResult HandleErrorResult(ControllerBase controller, ErrorType errorType, string? errorMessage)
        {
            return errorType switch
            {
                ErrorType.Conflict => controller.Conflict(new { Error = errorMessage }),
                ErrorType.NotFound => controller.NotFound(new { Error = errorMessage }),
                ErrorType.InvalidParameter => controller.BadRequest(new { Error = errorMessage }),
                _ => throw new Exception("An unhandled result has occurred as a result of a service call.")
            };
        }
    }
}
