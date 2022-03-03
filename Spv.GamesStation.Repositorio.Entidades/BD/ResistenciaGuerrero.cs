using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spv.GamesStation.Repositorio.Entidades.BD
{
    public class ResistenciaGuerrero
    {
        public int Id { get; set; }
        public int IdGuerrero { get; set; }
        public int IdNivel { get; set; }
        public int IdGrado { get; set; }
    }
}
