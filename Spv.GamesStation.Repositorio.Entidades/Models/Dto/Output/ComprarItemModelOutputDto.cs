using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spv.GamesStation.Repositorio.Entidades.Models.Dto.Output
{
    public class ComprarItemModelOutputDto
    {
        public Velocidad VelocidadGuerrero { get; set; }
        public Fuerza FuerzaGuerrero { get; set; }
        public Resistencia ResistenciaGuerrero { get; set; }
        public decimal MonedasDisponibles { get; set; }
        public int DiamantesDisponibles { get; set; }
    }
}
