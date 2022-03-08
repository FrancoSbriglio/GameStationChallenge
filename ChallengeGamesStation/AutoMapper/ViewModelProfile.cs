using AutoMapper;
using ChallengeGamesStation.Models.Input;
using ChallengeGamesStation.Models.Output;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;

namespace ChallengeGamesStation.AutoMapper
{
    public class ViewModelProfile : Profile
    {
        public ViewModelProfile()
        {
            CreateMap<ComprarItemModelInput, ComprarItemModelInputDto>();
            CreateMap<ComprarItemModelOutputDto, ComprarItemModelOutput>();
        }
    }
}
