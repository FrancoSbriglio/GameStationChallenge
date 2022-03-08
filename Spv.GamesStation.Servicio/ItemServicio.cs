using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spv.GamesStation.Dominio.Interfaz;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;
using Spv.GamesStation.Servicio.Interfaz;

namespace Spv.GamesStation.Servicio
{
    public class ItemServicio : IItemService
    {
        private readonly IItemDominio _itemDominio;
        public ItemServicio(IItemDominio itemDominio)
        {
            _itemDominio = itemDominio;
        }
        public async Task<ComprarItemModelOutputDto> ComprarItem(ComprarItemModelInputDto comprarItemModelInputDto)
        {
            return await _itemDominio.ComprarItem(comprarItemModelInputDto);
        }
    }
}
