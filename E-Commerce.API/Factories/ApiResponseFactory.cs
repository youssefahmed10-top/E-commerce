using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens.Experimental;
using Shared.ErrorModels;

namespace E_Commerce.API.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustamValidationErrorResponse (ActionContext Context)
        {
            //context ==> errors ,Key [field]
            //context.Modelstate ==> < string , ModelStateEntry >
            // ModelStateEntry ==> Errors ==> Error Message
            //IEnumrable<ValidationError>

            var Error = Context.ModelState.
                Where(E => E.Value?.Errors.Any() == true).Select(error => new ValidationErrors()
                {
                    field = error.Key,
                    Errors = error.Value?.Errors.Select(e => e.ErrorMessage) ?? new List<string>()

                });

            var Response = new ValidationErrorResponse()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Error = Error,
                ErrorMessage = "One Or More Vslidation error Happended"
            };

            return new BadRequestObjectResult(Response);
        }
    }
}
