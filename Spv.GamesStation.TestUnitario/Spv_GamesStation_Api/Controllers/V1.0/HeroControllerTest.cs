using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChallengeGamesStation.AutoMapper;
using ChallengeGamesStation.Controllers;
using ChallengeGamesStation.Models.Input;
using FluentAssertions;
using Moq;
using Spv.GamesStation.Repositorio.Entidades;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;
using Spv.GamesStation.Servicio.Interfaz;
using Xunit;

namespace Spv.GamesStation.TestUnitario.Spv_GamesStation_Api.Controllers.V1._0
{
    public class HeroControllerTest
    {
        private readonly HeroController _heroController;

        private readonly Mock<IItemService> _itemServiceMock;
        private readonly IMapper _mapper;

        public HeroControllerTest()
        {
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new ViewModelProfile()); });
            _mapper = mappingConfig.CreateMapper();

            _itemServiceMock = new Mock<IItemService>();
            _heroController = new HeroController(_mapper, _itemServiceMock.Object);
        }


        [Fact(DisplayName = "Cuando llamamos al servicio ItemComprar retorna resultado Ok")]
        public async Task Test001()
        {
            //Arrange
            var comprarItemModelInput = new ComprarItemModelInput
            {
                IdUsuario = 1,
                ValorItemMonedas = 10,
                DiamantesUsuario = 5,
                MonedasUsuario = 20,
                FuerzaGuerrero = new Fuerza(),
                ResistenciaGuerrero = new Resistencia(),
                VelocidadGuerrero = new Velocidad(),
                IdGuerrero = 1,
                IdItem = 1,
                IdTipoItem = 12,
                ValorItemDiamantes = 3
            };

            var comprarItemModelInputDto = new ComprarItemModelInputDto
            {
                IdUsuario = 1,
                ValorItemMonedas = 10,
                DiamantesUsuario = 5,
                MonedasUsuario = 20,
                FuerzaGuerrero = new Fuerza
                {
                    NumeroGrado = 1,
                    NumeroNivel = 2
                },
                ResistenciaGuerrero = new Resistencia
                {
                    NumeroGrado = 1,
                    NumeroNivel = 2
                },
                VelocidadGuerrero = new Velocidad
                {
                    NumeroGrado = 1,
                    NumeroNivel = 2
                },
                IdGuerrero = 1,
                IdItem = 1,
                IdTipoItem = 12,
                ValorItemDiamantes = 3
            };

            var comprarItemModelOutputDto = new ComprarItemModelOutputDto
            {
                VelocidadGuerrero = new Velocidad
                {
                    NumeroGrado = 1,
                    NumeroNivel = 2
                },
                DiamantesDisponibles = 5,
                FuerzaGuerrero = new Fuerza
                {
                    NumeroGrado = 1,
                    NumeroNivel = 2
                },
                MonedasDisponibles = 10,
                ResistenciaGuerrero = new Resistencia
                {
                    NumeroGrado = 1,
                    NumeroNivel = 2
                }
            };


            _itemServiceMock.Setup(x => x.ComprarItem(comprarItemModelInputDto)).ReturnsAsync(comprarItemModelOutputDto);

            //Act

            var comprarItem = await _heroController.ComprarItem(comprarItemModelInput);
            //Assert

            _itemServiceMock.Verify(x => x.ComprarItem(It.IsAny<ComprarItemModelInputDto>()));

            comprarItem.Should().NotBeNull();

        }

    }
}
