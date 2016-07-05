using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agenda.Entities
{
    [Table(name: "Compromissos")]
    public class Compromisso
    {
        public int ID { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.MultilineText)]
        public string Assunto { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Local { get; set; }

        [Required]
        [Display(Name = "Início")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime HoraInicio { get; set; }

        [Display(Name = "Fim")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime HoraFim { get; set; }

        public int AgendaID { get; set; }
    }
}
