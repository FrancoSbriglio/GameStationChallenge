using System.Net.Mime;
using AutoMapper;
using ChallengeGamesStation.Exception;
using ChallengeGamesStation.Models.Input;
using ChallengeGamesStation.Models.Output;
using Microsoft.AspNetCore.Mvc;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;
using Spv.GamesStation.Servicio.Interfaz;
using Swashbuckle.AspNetCore.Annotations;

namespace ChallengeGamesStation.Controllers
{
    [ApiVersion(ApiVersion)]
    [Route(RutaBase)]
    [ApiController]
    public class HeroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;
        public const string ApiVersion = "1.0";
        public const string RutaBase = "v{version:apiVersion}/hero";
        private const string RutaComprarItem = "item";

        public HeroController(IMapper mapper, IItemService itemService)
        {
            _mapper = mapper;
            _itemService = itemService;
        }


        [HttpPost(RutaComprarItem)]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "ComprarItem")]
        [SwaggerOperation(Summary = "Comprar Item para un guerrero",
            OperationId = "Comprar Item para un guerrero ",
            Tags = new[] { "Item" })]
        [ProducesResponseType(typeof(ComprarItemModelOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetailModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorDetailModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorDetailModel), StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json, "application/problem+json")]
        public async Task<IActionResult> ComprarItem(ComprarItemModelInput comprarItemModelInput)
        {

            var comprarItemModelInputDto =
                _mapper.Map<ComprarItemModelInput, ComprarItemModelInputDto>(comprarItemModelInput);

            ComprarItemModelOutputDto itemComprado = await _itemService.ComprarItem(comprarItemModelInputDto);

            return Ok(_mapper.Map<ComprarItemModelOutput>(itemComprado));

        }
    }
}
