using System.ComponentModel.DataAnnotations;
using Spv.GamesStation.Repositorio.Entidades.Models;

namespace GamesStationChallenge.Models.Input{

public class ComprarItemModelInput
{
    [Required(ErrorMessage = "El id Item es requerido")]
    public int IdItem { get; set; }
    [Required(ErrorMessage = "El id del usuario es requerido")]
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Las monedas que tiene el usuario son requeridas")]
        public decimal MonedasUsuario { get; set; }
        [Required(ErrorMessage = "Los diamantes que tiene el usuario son requeridas")]
        public int DiamantesUsuario { get; set; }
        [Required(ErrorMessage = "El valor del item en diamantes son requeridas")]
        [Range(1, 999999999999, ErrorMessage = "El rango varia entre 0 y 2147483647 ")]
        public int ValorItemDiamantes { get; set; }
        [Required(ErrorMessage = "El valor del item en monedas son requeridas")]
        public int ValorItemMonedas { get; set; }
        [Required(ErrorMessage = "El id del guerrero es requerido")]
        public int IdGuerrero { get; set; }
        [Required(ErrorMessage = "La fuerza del guerrero es requerida")]
        public Fuerza FuerzaGuerrero { get; set; }
        [Required(ErrorMessage = "La resistencia del guerrero es requerida")]
        public Resistencia ResistenciaGuerrero { get; set; }
        [Required(ErrorMessage = "La velocidad del guerrero es requerida")]
        public Velocidad VelocidadGuerrero { get; set; }
        [Required(ErrorMessage = "El tipo del item es requerido")]
        public int IdTipoItem { get; set; }
 

}
}