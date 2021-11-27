using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Cita
    {
            [Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoCita { get; set; }
        public string Hora{get; set;}
        public string Fecha{get; set;}
        public string IdPaciente{get; set;}
        [NotMapped]
        public Paciente Paciente{get; set;}
        public string IdPsicologo { get; set; }
        [NotMapped]
        public Psicologo Psicologo { get; set; }
        public double ValorCita { get; set; }
        
        public int Duracion { get; set; }
        public string Estado{get; set;}
        public int IdTratamiento { get; set; }
                             
        [NotMapped]

        public Tratamiento Tratamiento { get; set; }
        public string CodigoAgenda{get; set;}
        public string Motivo{get; set;}

    }
}