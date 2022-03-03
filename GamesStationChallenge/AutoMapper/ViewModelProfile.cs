using AutoMapper;
using GamesStationChallenge.Models.Input;
using GamesStationChallenge.Models.Output;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.InputDto;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.OutputDto;

namespace GamesStationChallenge.AutoMapper
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
