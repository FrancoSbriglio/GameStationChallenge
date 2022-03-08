using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChallengeGamesStation.Exception
{
    public interface IInvalidResponseBuilder
    {
        IActionResult Build(ActionExecutingContext context);
    }
}
