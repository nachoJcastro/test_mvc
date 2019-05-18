using System.ComponentModel.DataAnnotations;

namespace MVC_Eventos.Models
{
    public class ParticipanteEventoModel
    {
        [Required]
        public int EventoID { get; set; }

        [Required(ErrorMessage = "Ingrese Participantes")]
        public string Participante { get; set; }
    }
}