using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Spv.GamesStation.Dominio.Interfaz;
using Spv.GamesStation.Repositorio.Entidades;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input;
using Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output;
using Spv.GamesStation.Repositorio.Interfaz;
using Spv.GamesStation.Shared.Exceptions;

namespace Spv.GamesStation.Dominio
{
    public class ItemDominio : IItemDominio
    {
        private readonly IMapper _mapper;

        private readonly IItemRepositorio _itemRepositorio;
        private readonly IVelocidadGuerreroRepositorio _velocidadGuerreroRepositorio;
        private readonly IFuerzaGuerreroRepositorio _fuerzaGuerreroRepositorio;
        private readonly IResistenciaGuerrero _resistenciaGuerreroRepositorio;
        private IItemGuerreroRepositorio _itemGuerreroRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;

        public ItemDominio(IMapper mapper, IItemRepositorio itemRepositorio, IVelocidadGuerreroRepositorio velocidadGuerreroRepositorio, IFuerzaGuerreroRepositorio fuerzaGuerreroRepositorio, IResistenciaGuerrero resistenciaGuerreroRepositorio, IItemGuerreroRepositorio itemGuerreroRepositorio, IUsuarioRepositorio usuarioRepositorio)
        {
            _mapper = mapper;
            _itemRepositorio = itemRepositorio;
            _velocidadGuerreroRepositorio = velocidadGuerreroRepositorio;
            _fuerzaGuerreroRepositorio = fuerzaGuerreroRepositorio;
            _resistenciaGuerreroRepositorio = resistenciaGuerreroRepositorio;
            _itemGuerreroRepositorio = itemGuerreroRepositorio;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<ComprarItemModelOutputDto> ComprarItem(ComprarItemModelInputDto comprarItemModelInputDto)
        {
            await ValidarExistenciaItem(comprarItemModelInputDto.IdItem);

            switch (comprarItemModelInputDto.EsConDiamantes)
            {
                case false:
                    await ValidarCantidadDeMonedas(comprarItemModelInputDto.ValorItemMonedas, comprarItemModelInputDto.MonedasUsuario);
                    break;
                case true:
                    await ValidarCantidadDeDiamantes(comprarItemModelInputDto.ValorItemDiamantes,
                        comprarItemModelInputDto.DiamantesUsuario);
                    break;
            }

            await ValidarDiferenciaDeGradosEntreDestrezas(comprarItemModelInputDto.ResistenciaGuerrero,
                comprarItemModelInputDto.FuerzaGuerrero, comprarItemModelInputDto.VelocidadGuerrero);

            return await ComprarItemGuerrero(comprarItemModelInputDto);

            // en vez de este return abajo de este tendria uqe actualizarle las destresas a los personajes pero en el ejercicio no salen que item es de que destreza
            //y tambien mandarselas al frontend

        }

        private async Task<ComprarItemModelOutputDto> ComprarItemGuerrero(ComprarItemModelInputDto comprarItemModelInputDto)
        {
            decimal monedas = 0;
            int diamantes = 0;
            switch (comprarItemModelInputDto.EsConDiamantes)
            {
                case false:
                    monedas = await _usuarioRepositorio.DisminuirMonedas(comprarItemModelInputDto.MonedasUsuario - comprarItemModelInputDto.ValorItemMonedas, comprarItemModelInputDto.IdUsuario);
                    break;
                case true:

                    diamantes = await _usuarioRepositorio.DisminuirDiamantes(comprarItemModelInputDto.DiamantesUsuario - comprarItemModelInputDto.ValorItemDiamantes, comprarItemModelInputDto.IdUsuario);
                    break;
            }

            if (monedas == 0)
                monedas = comprarItemModelInputDto.MonedasUsuario;
            if (diamantes == 0)
                diamantes = comprarItemModelInputDto.DiamantesUsuario;

            await _itemGuerreroRepositorio.ComprarItemGuerrero(comprarItemModelInputDto.IdItem, comprarItemModelInputDto.IdGuerrero, comprarItemModelInputDto.IdTipoItem);

            return new ComprarItemModelOutputDto
            {
                MonedasDisponibles = monedas,
                DiamantesDisponibles = diamantes,
                FuerzaGuerrero = comprarItemModelInputDto.FuerzaGuerrero,
                ResistenciaGuerrero = comprarItemModelInputDto.ResistenciaGuerrero,
                VelocidadGuerrero = comprarItemModelInputDto.VelocidadGuerrero
            };
            // en vez de mandarle esto solo tendria uqe mandar solo el dinero tanto sea diamante como monedas y despeus arriba crear el objeto
            // y arriba setearle las destrezas
        }

        private async Task ValidarExistenciaItem(int idItem)
        {
            var existenciaItem = await _itemRepositorio.ValidarExistenciaItem(idItem);
            if (!existenciaItem)
                throw new BusinessException("El item que queres comprar no existe");
        }

        private async Task ValidarCantidadDeMonedas(decimal valorItemMonedas, decimal monedasUsuario)
        {
            if (monedasUsuario < valorItemMonedas)
                throw new BusinessException("No tenes las suficientes monedas para comprar el item");
        }

        private async Task ValidarCantidadDeDiamantes(decimal valorItemDiamantes, int diamantesUsuario)
        {
            if (diamantesUsuario < valorItemDiamantes)
                throw new BusinessException("No tenes los suficientes diamantes para comprar el item");
        }

        private async Task ValidarDiferenciaDeGradosEntreDestrezas(Resistencia resistenciaGuerrero, Fuerza fuerzaGuerrero,
            Velocidad velocidadGuerrero)
        {

            // todos empiezan en bronce 1

            if (resistenciaGuerrero.NumeroNivel == fuerzaGuerrero.NumeroNivel)
            {
                if (CalcularDifenreciadeGradoDeIgualNivel(resistenciaGuerrero.NumeroGrado, fuerzaGuerrero.NumeroGrado) > 5)
                    throw new BusinessException(
                        $"La diferencia del numero de grado de resistencia {resistenciaGuerrero.NumeroGrado}" +
                        $"y el numero de grado de la fuerza {fuerzaGuerrero.NumeroGrado} es mayor a 5");
            }

            if (resistenciaGuerrero.NumeroNivel == velocidadGuerrero.NumeroNivel)
            {
                if (CalcularDifenreciadeGradoDeIgualNivel(resistenciaGuerrero.NumeroGrado, velocidadGuerrero.NumeroGrado) > 5)
                    throw new BusinessException(
                        $"La diferencia del numero de grado de resistencia {resistenciaGuerrero.NumeroGrado}" +
                        $"y el numero de grado de la velocidad {velocidadGuerrero.NumeroGrado} es mayor a 5");
            }

            if (fuerzaGuerrero.NumeroNivel == velocidadGuerrero.NumeroNivel)
            {
                if (CalcularDifenreciadeGradoDeIgualNivel(fuerzaGuerrero.NumeroGrado, velocidadGuerrero.NumeroGrado) > 5)
                    throw new BusinessException(
                        $"La diferencia del numero de grado de fuerza {fuerzaGuerrero.NumeroGrado}" +
                        $"y el numero de grado de la velocidad {velocidadGuerrero.NumeroGrado} es mayor a 5");
            }

            if (resistenciaGuerrero.NumeroNivel != fuerzaGuerrero.NumeroNivel)
            {
                if (CalcularDifenreciadeGradoDeDiferenteNivel(resistenciaGuerrero.NumeroGrado, fuerzaGuerrero.NumeroGrado) > 5)
                    throw new BusinessException(
                        $"La diferencia del numero de grado de resistencia {resistenciaGuerrero.NumeroGrado}" +
                        $"y el numero de grado de la fuerza {fuerzaGuerrero.NumeroGrado} es mayor a 5");
            }

            if (resistenciaGuerrero.NumeroNivel != velocidadGuerrero.NumeroNivel)
            {
                if (CalcularDifenreciadeGradoDeDiferenteNivel(resistenciaGuerrero.NumeroGrado, fuerzaGuerrero.NumeroGrado) > 5)
                    throw new BusinessException(
                        $"La diferencia del numero de grado de resistencia {resistenciaGuerrero.NumeroGrado}" +
                        $"y el numero de grado de la fuerza {fuerzaGuerrero.NumeroGrado} es mayor a 5");
            }

            if (fuerzaGuerrero.NumeroNivel != velocidadGuerrero.NumeroNivel)
            {
                if (CalcularDifenreciadeGradoDeDiferenteNivel(fuerzaGuerrero.NumeroGrado, velocidadGuerrero.NumeroGrado) > 5)
                    throw new BusinessException(
                        $"La diferencia del numero de grado de fuerza {fuerzaGuerrero.NumeroGrado}" +
                        $"y el numero de grado de la velocidad {velocidadGuerrero.NumeroGrado} es mayor a 5");
            }

        }

        private int CalcularDifenreciadeGradoDeIgualNivel(int numeroGradoGenerico1, int numeroGradoGenerico2)
        {
            return Math.Abs(numeroGradoGenerico1 - numeroGradoGenerico2);
        }

        private int CalcularDifenreciadeGradoDeDiferenteNivel(int numeroGradoGenerico1, int numeroGradoGenerico2)
        {
            return (10 - numeroGradoGenerico1) + numeroGradoGenerico2;
        }
    }
}
