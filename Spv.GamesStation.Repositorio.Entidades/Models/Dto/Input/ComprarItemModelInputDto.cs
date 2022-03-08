using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spv.GamesStation.Repositorio.Entidades.Models.Dto.Input
{
    public class ComprarItemModelInputDto
    {
        public int IdItem { get; set; }
        public int ValorItemMonedas { get; set; }
        public int ValorItemDiamantes { get; set; }
        public int IdUsuario { get; set; }
        public int MonedasUsuario { get; set; }
        public int DiamantesUsuario { get; set; }
        public int IdGuerrero { get; set; }
        public Fuerza FuerzaGuerrero { get; set; }
        public Resistencia ResistenciaGuerrero { get; set; }
        public Velocidad VelocidadGuerrero { get; set; }
        public bool EsConDiamantes { get; set; }
        public int IdTipoItem { get; set; }
    }
}
