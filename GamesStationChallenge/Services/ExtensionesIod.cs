using AutoMapper;
using GamesStationChallenge.AutoMapper;

namespace GamesStationChallenge.Services
{
    public static class ExtensionesIod
    {
        public static void AgregarConfiguracionIod(this IServiceCollection services, IConfiguration configuration)
        {
            #region Autommaper

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new ViewModelProfile()); });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion



    }
}
}
