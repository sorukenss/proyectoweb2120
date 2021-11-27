using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Agenda
    {
        [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoAgenda  { get; set; }
        public bool Estado { get; set; }

        public string IdPsicologo { get; set; }

        public string Hora { get; set; }

        public string Dia { get; set; }

    }
}
