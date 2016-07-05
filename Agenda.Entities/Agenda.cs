using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda.Entities
{
    [Table(name: "Agendas")]
    public class Agenda
    {
        public int AgendaID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Titulo { get; set; }

        public string Cor { get; set; }
        public string Icone { get; set; }

        public virtual ICollection<Compromisso> Compromissos { get; set; }

        [Required]
        public string UserName { get; set; }

        public Agenda()
        {
            this.Compromissos = new List<Compromisso>();
        }
    }
}
