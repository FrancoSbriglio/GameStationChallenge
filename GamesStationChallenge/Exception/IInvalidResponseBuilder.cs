using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GamesStationChallenge.Exception
{
    public interface IInvalidResponseBuilder
    {
    
        IActionResult Build(ActionExecutingContext context);
    }
}
