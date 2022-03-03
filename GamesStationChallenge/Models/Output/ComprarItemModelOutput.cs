using Spv.GamesStation.Repositorio.Entidades.Models;

namespace GamesStationChallenge.Models.Output
{

    public class ComprarItemModelOutput
    {
        public Velocidad VelocidadGuerrero { get; set; }
        public Fuerza FuerzaGuerrero { get; set; }
        public Resistencia ResistenciaGuerrero { get; set; }
        public decimal MonedasDisponibles { get; set; }
        public int DiamantesDisponibles { get; set; }
    }
}