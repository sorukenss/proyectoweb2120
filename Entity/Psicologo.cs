using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Psicologo
    {
         [Key]
        public string CodigoPsicologo {get; set;}
        public Persona Persona {get; set;}
        public double Salario { get; set; }
        
        public List<Agenda> Agenda { get; set; }

        public Especialidad Especialidad { get; set; }
        
    }
}