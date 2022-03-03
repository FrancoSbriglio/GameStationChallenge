using GamesStationChallenge.Exception;
using GamesStationChallenge.Filters;
using GamesStationChallenge.Middleware;
using GamesStationChallenge.Providers;
using GamesStationChallenge.Services;
using GamesStationChallenge.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Spv.GamesStation.Repositorio;

namespace GamesStationChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AgregarConfiguracionIod(Configuration);
            ConfigureDataBaseService(services);
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorDetailModel), StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorDetailModel), StatusCodes.Status500InternalServerError));
                options.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorDetailModel), StatusCodes.Status422UnprocessableEntity));
                //options.Filters.Add(typeof(ModelStateValidateActionFilterAttribute));
                options.Filters.Add(typeof(NotFoundActionFilterAttribute));
                options.Filters.Add(typeof(ExceptionFilter));
            });

            services.AddVersionedApiExplorer(
                options =>
                {
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddApiVersioning(
                o =>
                {
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                    o.ReportApiVersions = true;
                    //o.ErrorResponses = new InvalidResponseBuilder();
                });

            services
                .AddMvc(opts => opts.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory()))
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                });

           
            SwaggerDefaultValues.AddSwaggerGenforService(services);
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public virtual void ConfigureDataBaseService(IServiceCollection services)
        {
            services.AddDbContext<HeroeContext>(options =>
                options
                    .UseInMemoryDatabase("HeroeDatabase"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
     
                app.UseSwagger(c => { c.SerializeAsV2 = true; });
                app.UseSwaggerUI(options => { SwaggerDefaultValues.SwaggerOptionUi(provider, options); });
            

            app.UseHttpsRedirection();

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
