using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class Paciente 
    {
      [Key]
        public string CodigoPaciente { get; set; }
        public Persona Persona{get; set;}
        public HistorialClinico Historial { get; set; }
         
        public List<Tratamiento> tratamientos  { get; set; }
        
        [NotMapped]
        public List<Cita> Citas { get; set; }
    }
}
