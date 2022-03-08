using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ChallengeGamesStation.Swagger
{
    [ExcludeFromCodeCoverage]
    public class SwaggerDefaultValues : IOperationFilter
    {
        private static readonly string uri = "https://www.GamesStation.com.ar";

   
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {



            if (!operation.Parameters.Select(p => p.In != ParameterLocation.Header).Any())
                return;



            foreach (var parameter in operation.Parameters)
            {
                if (parameter.In == ParameterLocation.Header)
                    continue;



                var description = context.ApiDescription.ParameterDescriptions.First(p => string.Equals(p.Name, parameter.Name, StringComparison.InvariantCultureIgnoreCase));
                var routeInfo = description.RouteInfo;



                if (string.IsNullOrEmpty(parameter.Name))
                {
                    parameter.Name = description.ModelMetadata?.Name;
                }



                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }



                if (routeInfo == null)
                {
                    continue;
                }



                parameter.Required |= !routeInfo.IsOptional;
            }
        }

     
        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var version = "1.0.0";

            try
            {
                var fileName = Path.Combine(
                    Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty,
                    "VERSION");
                if (File.Exists(fileName)) version = File.ReadLines(fileName).FirstOrDefault() ?? version;
            }
            catch
            {
                version = "1.0.0";
            }

            var info = new OpenApiInfo
            {
                Description = $"API GamesStation {version}",
                Title = "Games Station",

                Version = version,
                Contact = new OpenApiContact
                { Name = "Developer", Email = "franco.sbriglio@gmail.com" },
                TermsOfService = new Uri(uri)
            };




            if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

            return info;
        }


        public static void AddSwaggerGenforService(IServiceCollection services)
        {
            services.AddSwaggerGen(
                options =>
                {
                    var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                    foreach (var description in provider.ApiVersionDescriptions)
                        options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));

                    options.OperationFilter<SwaggerDefaultValues>();

                    options.DescribeAllParametersInCamelCase();
                    options.EnableAnnotations();
                    options.CustomSchemaIds(type => type.FullName);
                    options.SchemaFilter<CustomNameSchema>();
                    options.UseInlineDefinitionsForEnums();
                    options.DescribeAllEnumsAsStrings();
                });
        }

    
        public static void SwaggerOptionUi(IApiVersionDescriptionProvider provider, SwaggerUIOptions options)
        {
            // build a swagger endpoint for each discovered API version
            foreach (var description in provider.ApiVersionDescriptions)
                options.SwaggerEndpoint(
                    $"../swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());

            options.RoutePrefix = "swagger";
        }


    }
}
