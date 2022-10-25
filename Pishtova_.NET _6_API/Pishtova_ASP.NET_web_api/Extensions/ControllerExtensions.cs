namespace Pishtova_ASP.NET_web_api.Extensions
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    using Pishtova.Data.Common.Utilities;

    public static class ControllerExtensions
    {
        public static ActionResult Error(this ControllerBase controller, OperationResult operationResult)
        {
            if (controller is null) throw new ArgumentNullException(nameof(controller));
            if (operationResult is null) throw new ArgumentNullException(nameof(operationResult));

            var statusCode = operationResult.Errors.Any(e => e.IsNotExpected) ? StatusCodes.Status500InternalServerError : StatusCodes.Status400BadRequest;
            return controller.Problem(operationResult.ToString(), controller.Request.Path, statusCode, "Your actions was not executed successfully.");
        }
    }
}
