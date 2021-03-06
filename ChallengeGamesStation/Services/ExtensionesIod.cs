using AutoMapper;
using ChallengeGamesStation.AutoMapper;
using Spv.GamesStation.Dominio;
using Spv.GamesStation.Dominio.Interfaz;
using Spv.GamesStation.Repositorio;
using Spv.GamesStation.Repositorio.Interfaz;
using Spv.GamesStation.Servicio;
using Spv.GamesStation.Servicio.Interfaz;

namespace ChallengeGamesStation.Services
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

            services.AddTransient<IItemService, ItemServicio>();
            services.AddTransient<IItemDominio, ItemDominio>();

            services.AddTransient<IItemRepositorio, ItemRepositorio>();

            services.AddTransient<IVelocidadGuerreroRepositorio, VelocidadGuerreroRepositorio>();
            services.AddTransient<IFuerzaGuerreroRepositorio, FuerzaGuerreroRepositorio>();
            services.AddTransient<IResistenciaGuerrero, ResistenciaGuerreroRepositorio>();
            services.AddTransient<IItemGuerreroRepositorio, ItemGuerreroRepositorio>();
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();

        }
    }
}
