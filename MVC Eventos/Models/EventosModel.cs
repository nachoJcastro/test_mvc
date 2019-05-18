using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_Eventos.Models
{
    public class EventosModel
    {
        public EventosModel()
        {
            ParticipanteEvento = new List<ParticipanteEventoModel>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventoID { get; set; }
        [Required(ErrorMessage = "Ingrese título")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Ingrese Descripción")]
        public string Descripcion { get; set; }
        public Nullable<int> Estado { get; set; }

        [Required(ErrorMessage = "Ingrese Participantes")]
        [Display(Name = "Participantes")]
        public virtual List<ParticipanteEventoModel> ParticipanteEvento { get ; set; }
    }
}