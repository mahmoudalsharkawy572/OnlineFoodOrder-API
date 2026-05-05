using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_Commerce.Web.Factories
{
    public static class ApiResponseFactory
    { 
        public static IActionResult GenerateApiValidationErrorsResponse (ActionContext Context)
        {
            var errors = Context.ModelState
                            .Where(e => e.Value.Errors.Any())
                            .Select(e => new ValidationError
                            {
                                Field = e.Key,
                                Errors = e.Value.Errors.Select(x => x.ErrorMessage)
                            });
            var Response = new ValidationErrorToReturn()
            {
                ValidationErrors = errors
            };
            return new BadRequestObjectResult(Response);
        }
    }
}
