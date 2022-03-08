using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChallengeGamesStation.Exception
{
    public class InvalidResponseBuilder : IInvalidResponseBuilder
    {
   
        public IActionResult Build(ActionExecutingContext context)
        {
            var response = new ErrorDetailModel
            {
                State = HttpStatusCode.BadRequest.ToString(),
                Code = (int)HttpStatusCode.BadRequest,
                Detail = "Se produjeron uno o más errores de validación."
            };
            foreach (var error in context.ModelState.SelectMany(item => item.Value.Errors))
            {
                response.Errors.Add(new Error
                {
                    Code = ((int)HttpStatusCode.BadRequest).ToString(),
                    Title = "El valor enviado es invalido",
                    Detail = error.ErrorMessage,
                    Source = context.ActionDescriptor.DisplayName,
                    SpvTrackId = context.HttpContext.TraceIdentifier
                });
            }

            return new BadRequestObjectResult(response);
        }
    }
}
