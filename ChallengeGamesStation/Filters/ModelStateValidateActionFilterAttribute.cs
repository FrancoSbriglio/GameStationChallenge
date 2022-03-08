using System.Diagnostics.CodeAnalysis;
using ChallengeGamesStation.Exception;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ChallengeGamesStation.Filters
{
    [ExcludeFromCodeCoverage]
    public class ModelStateValidateActionFilterAttribute : ActionFilterAttribute
    {
        private readonly IInvalidResponseBuilder _invalidResponseBuilder;

        /// <summary>
        /// ModelStateValidateActionFilterAttribute
        /// </summary>
        /// <param name="invalidResponseBuilder"></param>
        public ModelStateValidateActionFilterAttribute(IInvalidResponseBuilder invalidResponseBuilder)
        {
            _invalidResponseBuilder = invalidResponseBuilder;
        }

        /// <summary>
        /// OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = _invalidResponseBuilder.Build(context);
            }
        }
    }
}
