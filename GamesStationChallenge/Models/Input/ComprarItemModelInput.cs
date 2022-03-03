using Spv.GamesStation.Repositorio.Entidades.Models;

namespace GamesStationChallenge.Models.Input{

public class ComprarItemModelInput
{
    public int IdItem { get; set; }
    public int IdUsuario { get; set; }
    public decimal MonedasUsuario { get; set; }
    public int DiamantesUsuario { get; set; }
    public int ValorItemDiamantes { get; set; }
    public int ValorItemMonedas { get; set; }
    public int IdGuerrero { get; set; }
    public Fuerza FuerzaGuerrero { get; set; }
    public Resistencia ResistenciaGuerrero { get; set; }
    public Velocidad VelocidadGuerrero { get; set; }

    public int IdTipoItem { get; set; }
 

}
}