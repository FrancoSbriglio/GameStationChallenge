using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;

namespace Spv.GamesStation.Servicio.Interfaz
{
    public interface IItemService
    {
        Task<ComprarItemModelOutputDto> ComprarItem(ComprarItemModelInputDto comprarItemModelInputDto);
    }
}
