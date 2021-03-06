using System.Diagnostics.CodeAnalysis;
using System.Net;
using ChallengeGamesStation.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ChallengeGamesStation.Filters
{
    [ExcludeFromCodeCoverage]
    public class NotFoundActionFilterAttribute : ActionFilterAttribute
    {
     
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var response = new ErrorDetailModel
            {
                State = HttpStatusCode.NotFound.ToString(),
                Code = (int)HttpStatusCode.NotFound,
                // Type =TipoExcepcionEnum.Negocio.ObtenerDescripcion(),
                Detail = "No se encontró el recurso buscado."
            };


            if (context.Result is NotFoundResult)
            {
                ContentResult contres = new ContentResult();
                contres.Content = JsonConvert.SerializeObject(response);
                contres.ContentType = "application/json";
                contres.StatusCode = (int)HttpStatusCode.NotFound;

                context.Result = contres;
            }
        }
    }
}
