using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Spv.GamesStation.Dominio.Interfaz;
using Spv.GamesStation.Repositorio.Entidades;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;
using Spv.GamesStation.Servicio;
using Xunit;

namespace Spv.GamesStation.TestUnitario.Spv_Servicio
{
    public class ItemServicioTest
    {
        public ItemServicio _itemServicio { get; set; }
        public Mock<IItemDominio> _itemDominioMock { get; set; }
        public ItemServicioTest()
        {
            _itemDominioMock = new Mock<IItemDominio>();
            _itemServicio = new ItemServicio(_itemDominioMock.Object);
        }

        [Fact(DisplayName = "Cuando Se  llama a comprar item del dominio Entonces se verifica que se llame correctamente")]
        public async Task Test001()
        {
            // Arranged
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

            _itemDominioMock.Setup(x => x.ComprarItem(comprarItemModelInputDto))
                .ReturnsAsync(comprarItemModelOutputDto);

            // Act


            var resultado = await _itemServicio.ComprarItem(comprarItemModelInputDto);

            // Assert
            _itemDominioMock.Verify(x => x.ComprarItem(It.IsAny<ComprarItemModelInputDto>()));
            resultado.Should().NotBeNull();


        }
    }
}
