using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChallengeGamesStation.AutoMapper;
using FluentAssertions;
using Moq;
using Spv.GamesStation.Dominio;
using Spv.GamesStation.Repositorio.Entidades;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;
using Spv.GamesStation.Repositorio.Interfaz;
using Spv.GamesStation.Shared.Exceptions;
using Xunit;

namespace Spv.GamesStation.TestUnitario.Spv_Dominio
{
    public class ItemDominioTest
    {
        public ItemDominio _itemDominio { get; set; }
        public Mock<IItemRepositorio> _itemRepositorioMock { get; set; }
        private Mock<IVelocidadGuerreroRepositorio> _velocidadGuerrerRepositorioMock;
        private Mock<IFuerzaGuerreroRepositorio> _fuerzaGuerreroRepositorioMock;
        private Mock<IResistenciaGuerrero> _resistenciaGuerreroRepositorioMock;
        private Mock<IItemGuerreroRepositorio> _itemGuerreroRepositorioMock;
        private Mock<IUsuarioRepositorio> _usuarioRepositorioMock;

        public ItemDominioTest()
        {

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new ViewModelProfile()); });
            var mapper = mappingConfig.CreateMapper();

            _itemRepositorioMock = new Mock<IItemRepositorio>();
            _velocidadGuerrerRepositorioMock = new Mock<IVelocidadGuerreroRepositorio>();
            _fuerzaGuerreroRepositorioMock = new Mock<IFuerzaGuerreroRepositorio>();
            _resistenciaGuerreroRepositorioMock = new Mock<IResistenciaGuerrero>();
            _itemGuerreroRepositorioMock = new Mock<IItemGuerreroRepositorio>();
            _usuarioRepositorioMock = new Mock<IUsuarioRepositorio>();


            _itemDominio = new ItemDominio(mapper, _itemRepositorioMock.Object, _velocidadGuerrerRepositorioMock.Object, _fuerzaGuerreroRepositorioMock.Object, _resistenciaGuerreroRepositorioMock.Object, _itemGuerreroRepositorioMock.Object, _usuarioRepositorioMock.Object);
        }

        [Fact(DisplayName = "Cuando Se  llama a comprar item del repositorio y queiro pagar con monedas Entonces se verifica que se llame correctamente")]
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
                ValorItemDiamantes = 3,
                EsConDiamantes = false
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

            var monedas = comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas;

            _itemRepositorioMock.Setup(x => x.ValidarExistenciaItem(comprarItemModelInputDto.IdItem)).ReturnsAsync(true);

            _usuarioRepositorioMock.Setup(x => x.DisminuirMonedas(monedas, comprarItemModelInputDto.IdUsuario))
                .ReturnsAsync(monedas);

            // Act


            var resultado = await _itemDominio.ComprarItem(comprarItemModelInputDto);

            // Assert
            _usuarioRepositorioMock.Verify(x => x.DisminuirMonedas(It.IsAny<int>(), It.IsAny<int>()));
            resultado.Should().NotBeNull();


        }



        [Fact(DisplayName = "Cuando Se llama a comprar item del repositorio y no existe el item Entonces se lanza excepcion")]
        public async Task Test002()
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
                ValorItemDiamantes = 3,
                EsConDiamantes = false
            };


            var monedas = comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas;

            _itemRepositorioMock.Setup(x => x.ValidarExistenciaItem(comprarItemModelInputDto.IdItem)).ReturnsAsync(false);

            _usuarioRepositorioMock.Setup(x => x.DisminuirMonedas(monedas, comprarItemModelInputDto.IdUsuario))
                .ReturnsAsync(monedas);

            // Act

            Func<Task<ComprarItemModelOutputDto>> func = async () => await _itemDominio.ComprarItem(comprarItemModelInputDto);

            // Assert

            await func.Should().ThrowAsync<BusinessException>().WithMessage("El item que queres comprar no existe");


        }


        [Fact(DisplayName = "Cuando Se  llama a comprar item del repositorio y queiro pagar con diamantes Entonces se verifica que se llame correctamente")]
        public async Task Test003()
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
                ValorItemDiamantes = 3,
                EsConDiamantes = true
            };


            var monedas = comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas;

            _itemRepositorioMock.Setup(x => x.ValidarExistenciaItem(comprarItemModelInputDto.IdItem)).ReturnsAsync(false);

            _usuarioRepositorioMock.Setup(x => x.DisminuirMonedas(monedas, comprarItemModelInputDto.IdUsuario))
                .ReturnsAsync(monedas);

            // Act

            Func<Task<ComprarItemModelOutputDto>> func = async () => await _itemDominio.ComprarItem(comprarItemModelInputDto);

            // Assert

            await func.Should().ThrowAsync<BusinessException>().WithMessage("El item que queres comprar no existe");


        }


        [Fact(DisplayName = "Cuando Se llama a comprar item del repositorio y existe direncia de destrezas Entonces se lanza excepcion")]
        public async Task Test004()
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
                    NumeroGrado = 8,
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
                ValorItemDiamantes = 3,
                EsConDiamantes = false
            };


            var monedas = comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas;

            _itemRepositorioMock.Setup(x => x.ValidarExistenciaItem(comprarItemModelInputDto.IdItem)).ReturnsAsync(true);

            _usuarioRepositorioMock.Setup(x => x.DisminuirMonedas(monedas, comprarItemModelInputDto.IdUsuario))
                .ReturnsAsync(monedas);

            // Act

            Func<Task<ComprarItemModelOutputDto>> func = async () => await _itemDominio.ComprarItem(comprarItemModelInputDto);

            // Assert

            await func.Should().ThrowAsync<BusinessException>().WithMessage("La diferencia del numero de grado de resistencia 8y el numero de grado de la fuerza 1 es mayor a 5");


        }


        [Fact(DisplayName = "Cuando Se llama a comprar item del repositorio y existe direncia de destrezas con diferente nivel Entonces se lanza excepcion")]
        public async Task Test005()
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
                    NumeroGrado = 8,
                    NumeroNivel = 2
                },
                ResistenciaGuerrero = new Resistencia
                {
                    NumeroGrado = 8,
                    NumeroNivel = 2
                },
                VelocidadGuerrero = new Velocidad
                {
                    NumeroGrado = 4,
                    NumeroNivel = 3
                },
                IdGuerrero = 1,
                IdItem = 1,
                IdTipoItem = 12,
                ValorItemDiamantes = 3,
                EsConDiamantes = false
            };


            var monedas = comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas;

            _itemRepositorioMock.Setup(x => x.ValidarExistenciaItem(comprarItemModelInputDto.IdItem)).ReturnsAsync(true);

            _usuarioRepositorioMock.Setup(x => x.DisminuirMonedas(monedas, comprarItemModelInputDto.IdUsuario))
                .ReturnsAsync(monedas);

            // Act

            Func<Task<ComprarItemModelOutputDto>> func = async () => await _itemDominio.ComprarItem(comprarItemModelInputDto);

            // Assert

            await func.Should().ThrowAsync<BusinessException>().WithMessage("La diferencia del numero de grado de resistencia 8y el numero de grado de la fuerza 8 es mayor a 5");


        }

        [Fact(DisplayName = "Cuando Se llama a comprar item del repositorio y no tengo la cantidad de monedas necesarias nivel Entonces se lanza excepcion")]
        public async Task Test006()
        {
            // Arranged
            var comprarItemModelInputDto = new ComprarItemModelInputDto
            {
                IdUsuario = 1,
                ValorItemMonedas = 10,
                DiamantesUsuario = 5,
                MonedasUsuario = 5,
                FuerzaGuerrero = new Fuerza
                {
                    NumeroGrado = 8,
                    NumeroNivel = 2
                },
                ResistenciaGuerrero = new Resistencia
                {
                    NumeroGrado = 8,
                    NumeroNivel = 2
                },
                VelocidadGuerrero = new Velocidad
                {
                    NumeroGrado = 4,
                    NumeroNivel = 3
                },
                IdGuerrero = 1,
                IdItem = 1,
                IdTipoItem = 12,
                ValorItemDiamantes = 3,
                EsConDiamantes = false
            };


            var monedas = comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas;

            _itemRepositorioMock.Setup(x => x.ValidarExistenciaItem(comprarItemModelInputDto.IdItem)).ReturnsAsync(true);

            _usuarioRepositorioMock.Setup(x => x.DisminuirMonedas(monedas, comprarItemModelInputDto.IdUsuario))
                .ReturnsAsync(monedas);

            // Act

            Func<Task<ComprarItemModelOutputDto>> func = async () => await _itemDominio.ComprarItem(comprarItemModelInputDto);

            // Assert

            await func.Should().ThrowAsync<BusinessException>().WithMessage("No tenes las suficientes monedas para comprar el item");


        }


        [Fact(DisplayName = "Cuando Se llama a comprar item del repositorio y no tengo la cantidad de diamantes necesarias  Entonces se lanza excepcion")]
        public async Task Test007()
        {
            // Arranged
            var comprarItemModelInputDto = new ComprarItemModelInputDto
            {
                IdUsuario = 1,
                ValorItemMonedas = 10,
                DiamantesUsuario = 1,
                MonedasUsuario = 5,
                FuerzaGuerrero = new Fuerza
                {
                    NumeroGrado = 8,
                    NumeroNivel = 2
                },
                ResistenciaGuerrero = new Resistencia
                {
                    NumeroGrado = 8,
                    NumeroNivel = 2
                },
                VelocidadGuerrero = new Velocidad
                {
                    NumeroGrado = 4,
                    NumeroNivel = 3
                },
                IdGuerrero = 1,
                IdItem = 1,
                IdTipoItem = 12,
                ValorItemDiamantes = 3,
                EsConDiamantes = true
            };


            var monedas = comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas;

            _itemRepositorioMock.Setup(x => x.ValidarExistenciaItem(comprarItemModelInputDto.IdItem)).ReturnsAsync(true);

            _usuarioRepositorioMock.Setup(x => x.DisminuirMonedas(monedas, comprarItemModelInputDto.IdUsuario))
                .ReturnsAsync(monedas);

            // Act

            Func<Task<ComprarItemModelOutputDto>> func = async () => await _itemDominio.ComprarItem(comprarItemModelInputDto);

            // Assert

            await func.Should().ThrowAsync<BusinessException>().WithMessage("No tenes los suficientes diamantes para comprar el item");


        }

    }
}
