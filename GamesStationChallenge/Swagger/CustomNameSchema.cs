using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GamesStationChallenge.Swagger
{
    [ExcludeFromCodeCoverage]
    public class CustomNameSchema : ISchemaFilter
    {
        /// <summary>
        ///     Apply
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null) return;

            var objAttribute = context.Type.GetCustomAttribute<JsonObjectAttribute>();
            if (objAttribute != null && objAttribute.Title?.Length > 0) schema.Title = objAttribute.Title;
        }
    }
}
